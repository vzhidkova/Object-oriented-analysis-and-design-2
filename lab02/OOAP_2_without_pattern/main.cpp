#include <SFML/Graphics.hpp>
#include <iostream>
#include <vector>
#include <memory>
#include <stack>
#include <cmath>

// Конфигурация
const int WINDOW_WIDTH = 1000;
const int WINDOW_HEIGHT = 800;
const int UI_HEIGHT = 100;
const int CANVAS_WIDTH = 1000;
const int CANVAS_HEIGHT = 700;
const int TILE_SIZE = 64;

const int NUM_COLS = (CANVAS_WIDTH + TILE_SIZE - 1) / TILE_SIZE;
const int NUM_ROWS = (CANVAS_HEIGHT + TILE_SIZE - 1) / TILE_SIZE;

// --- 1. TILE (Flyweight) ---
class Tile {
public:
    sf::Image image;
    sf::Texture texture;
    sf::Sprite sprite;
    bool is_dirty = true;

    Tile() :
        image({ (unsigned int)TILE_SIZE, (unsigned int)TILE_SIZE }, sf::Color::White),
        texture(image),  // В SFML 3 текстура может принимать Image в конструкторе
        sprite(texture)  // Передаем текстуру сразу, решаем ошибку E0291
    {
        // В теле конструктора только донастройка
        sprite.setTexture(texture);
    }

    // Конструктор копирования тоже нужно обновить
    Tile(const Tile& other) :
        image(other.image),
        texture(image),
        sprite(texture),
        is_dirty(true)
    {
        sprite.setPosition(other.sprite.getPosition());
    }

    void setPixelLocal(int x, int y, sf::Color color) {
        if (x >= 0 && x < (int)image.getSize().x && y >= 0 && y < (int)image.getSize().y) {
            image.setPixel({ (unsigned int)x, (unsigned int)y }, color);
            is_dirty = true;
        }
    }

    void updateTextureIfDirty() {
        if (is_dirty) {
            texture.update(image);
            is_dirty = false;
        }
    }
};

// --- 2. CANVASSNAPSHOT (Proxy) ---
class CanvasSnapshot {
private:
    std::vector<std::shared_ptr<Tile>> tiles_grid;

public:
    CanvasSnapshot() {
        tiles_grid.reserve(NUM_COLS * NUM_ROWS);
        for (int row = 0; row < NUM_ROWS; ++row) {
            for (int col = 0; col < NUM_COLS; ++col) {
                auto tile = std::make_shared<Tile>();
                tile->sprite.setPosition({ (float)col * TILE_SIZE, (float)row * TILE_SIZE });
                tiles_grid.push_back(tile);
            }
        }
    }

    void paintPixel(int gx, int gy, sf::Color color) {
        int col = gx / TILE_SIZE;
        int row = gy / TILE_SIZE;
        if (col < 0 || col >= NUM_COLS || row < 0 || row >= NUM_ROWS) return;

        auto& tile_ptr = tiles_grid[row * NUM_COLS + col];
        if (tile_ptr.use_count() > 1) {
            tile_ptr = std::make_shared<Tile>(*tile_ptr);
        }
        tile_ptr->setPixelLocal(gx % TILE_SIZE, gy % TILE_SIZE, color);
    }

    void draw(sf::RenderWindow& window) {
        for (auto& tile_ptr : tiles_grid) {
            tile_ptr->updateTextureIfDirty();
            window.draw(tile_ptr->sprite);
        }
    }
};

// --- 3. UNDO MANAGER ---
struct UndoManager {
    CanvasSnapshot active_snapshot;
    std::stack<CanvasSnapshot> history;

    void commit() { history.push(active_snapshot); }
    void undo() {
        if (!history.empty()) {
            active_snapshot = history.top();
            history.pop();
        }
    }
};

int main() {
    // В SFML 3 VideoMode принимает Vector2u
    sf::RenderWindow window(sf::VideoMode({ WINDOW_WIDTH, WINDOW_HEIGHT }), "SFML 3.0 Tiled Canvas");
    window.setFramerateLimit(60);

    sf::Font font;
    // В SFML 3 метод называется openFromFile
    if (!font.openFromFile("arial.ttf")) {
        std::cout << "Font not found!\n";
    }

    UndoManager undoManager;
    sf::Color current_brush_color = sf::Color::Red;
    bool is_drawing = false;

    // Список цветов для палитры
    std::vector<sf::Color> palette_colors = { sf::Color::Red, sf::Color::Green, sf::Color::Blue, sf::Color::Black, sf::Color::Yellow };
    std::vector<sf::RectangleShape> palette_buttons;

    float btn_size = 40.0f;
    float start_x = 20.0f;
    float start_y = (float)WINDOW_HEIGHT - UI_HEIGHT + 20.0f;

    for (size_t i = 0; i < palette_colors.size(); ++i) {
        // В SFML 3.0 конструктор принимает sf::Vector2f
        sf::RectangleShape btn({ btn_size, btn_size });
        btn.setFillColor(palette_colors[i]);
        btn.setOutlineThickness(2.0f);
        btn.setOutlineColor(sf::Color::White);
        btn.setPosition({ start_x + i * (btn_size + 15.0f), start_y });
        palette_buttons.push_back(btn);
    }

    // Помечаем выбранный цвет (например, красный) желтой рамкой
    palette_buttons[0].setOutlineColor(sf::Color::Magenta);

    // UI
    sf::RectangleShape ui_panel({ (float)WINDOW_WIDTH, (float)UI_HEIGHT });
    ui_panel.setFillColor(sf::Color(200, 200, 200));
    ui_panel.setPosition({ 0, (float)WINDOW_HEIGHT - UI_HEIGHT });

    // Кнопка Undo
    sf::RectangleShape undo_btn({ 100.f, 40.f });
    undo_btn.setFillColor(sf::Color::Black);
    undo_btn.setPosition({ (float)WINDOW_WIDTH - 120, (float)WINDOW_HEIGHT - 70 });

    while (window.isOpen()) {
        // 1. ОБРАБОТКА СОБЫТИЙ
        while (const std::optional event = window.pollEvent()) {
            if (event->is<sf::Event::Closed>()) {
                window.close();
            }

            // Нажатие кнопки мыши
            if (const auto* mousePressed = event->getIf<sf::Event::MouseButtonPressed>()) {
                if (mousePressed->button == sf::Mouse::Button::Left) {
                    sf::Vector2i pos = mousePressed->position;
                    sf::Vector2f fPos = { (float)pos.x, (float)pos.y };

                    // Если кликнули по холсту (выше панели инструментов)
                    if (pos.y < CANVAS_HEIGHT) {
                        is_drawing = true;
                        undoManager.commit(); // Сохраняем состояние для Undo
                    }
                    // Если кликнули по нижней панели
                    else {
                        // Проверка кнопок палитры
                        for (size_t i = 0; i < palette_buttons.size(); ++i) {
                            if (palette_buttons[i].getGlobalBounds().contains(fPos)) {
                                current_brush_color = palette_colors[i];
                                for (auto& b : palette_buttons) b.setOutlineColor(sf::Color::White);
                                palette_buttons[i].setOutlineColor(sf::Color::Yellow);
                            }
                        }
                        // Проверка кнопки Undo
                        if (undo_btn.getGlobalBounds().contains(fPos)) {
                            undoManager.undo();
                        }
                    }
                }
            }

            // Отпускание кнопки мыши
            if (event->is<sf::Event::MouseButtonReleased>()) {
                is_drawing = false;
            }
        }

        // 2. ЛОГИКА РИСОВАНИЯ (пока зажата кнопка)
        if (is_drawing) {
            sf::Vector2i m_pos = sf::Mouse::getPosition(window);
            // Рисуем небольшим квадратом 5x5 для видимости
            for (int dx = -2; dx <= 2; ++dx) {
                for (int dy = -2; dy <= 2; ++dy) {
                    undoManager.active_snapshot.paintPixel(m_pos.x + dx, m_pos.y + dy, current_brush_color);
                }
            }
        }

        // 3. ОТРИСОВКА (СТРОГИЙ ПОРЯДОК)

        // А. Очищаем задний буфер в белый цвет
        window.clear(sf::Color::White);

        // Б. Рисуем холст (тайлы) — это САМЫЙ нижний слой
        undoManager.active_snapshot.draw(window);

        // В. Рисуем серую панель инструментов поверх холста
        window.draw(ui_panel);

        // Г. Рисуем кнопку Undo и кнопки цветов
        window.draw(undo_btn);
        for (const auto& btn : palette_buttons) {
            window.draw(btn);
        }

        // Д. Переключаем буферы (выводим всё на экран за один раз)
        window.display();
    }
    return 0;
}