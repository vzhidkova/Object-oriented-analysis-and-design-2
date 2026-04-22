import tkinter as tk
from tkinter import ttk, messagebox
from PIL import Image, ImageTk, ImageDraw
import heapq

# --- Логика алгоритма ---
def dijkstra_with_filter(graph, start_node, end_node, road_type):
    distances = {node: float('inf') for node in graph}
    distances[start_node] = 0
    predecessors = {node: None for node in graph}
    priority_queue = [(0, start_node)]
    
    while priority_queue:
        current_distance, u = heapq.heappop(priority_queue)
        if current_distance > distances[u]:
            continue
        if u == end_node:
            break
        for weight, path_type, v in graph.get(u, []):
            if path_type in road_type:
                distance = current_distance + weight
                if distance < distances[v]:
                    distances[v] = distance
                    predecessors[v] = u
                    heapq.heappush(priority_queue, (distance, v))
    
    path = []
    current = end_node
    if distances[end_node] == float('inf'):
        return [], float('inf')
    while current is not None:
        path.append(current)
        current = predecessors[current]
    return path[::-1], distances[end_node]

# --- Данные ---
my_graph = {
    '0': [[2, 'l', '1'], [1, 'd', '12']], '1': [[1, 'l', '0'], [3, 'l', '6'], [1, 'd', '12']],
    '2': [[1, 't', '3'], [1, 'd', '19']], '3': [[1, 't', '2'], [1, 't', '4'], [2, 'l', '9'], [1, 'd', '14']],
    '4': [[1, 't', '3'], [1, 'd', '15']], '5': [[1, 't', '6'], [1, 't', '7'], [1, 'd', '17']],
    '6': [[3, 'l', '1'], [1, 't', '5'], [1, 't', '8'], [1, 'd', '21']], '7': [[1, 't', '5'], [1, 't', '8'], [1, 'd', '23']],
    '8': [[1, 't', '6'], [1, 't', '7'], [2, 't', '10'], [1, 'd', '22']], '9': [[2, 'l', '3'], [1, 't', '11'], [1, 'd', '20']],
    '10': [[2, 't', '8'], [1, 't', '11']], '11': [[1, 't', '9'], [1, 't', '10'], [1, 'd', '26']],
    '12': [[1, 'd', '0'], [3, 'd', '13']], '13': [[3, 'd', '12'], [3, 'd', '14'], [3, 'd', '16']],
    '14': [[1, 'd', '3'], [3, 'd', '13'], [2, 'd', '15']], '15': [[1, 'd', '4'], [2, 'd', '14']],
    '16': [[1, 'd', '1'], [3, 'd', '13'], [1, 'd', '18']], '17': [[1, 'd', '5'], [4, 'd', '18']],
    '18': [[1, 'd', '16'], [4, 'd', '17'], [1, 'd', '19'], [1, 'd', '21']], '19': [[1, 'd', '2'], [1, 'd', '18'], [1, 'd', '20']],
    '20': [[1, 'd', '9'], [1, 'd', '19']], '21': [[1, 'd', '6'], [1, 'd', '18'], [2, 'd', '22']],
    '22': [[1, 'd', '8'], [2, 'd', '21'], [1, 'd', '24']], '23': [[1, 'd', '7'], [4, 'd', '24']],
    '24': [[1, 'd', '22'], [4, 'd', '23'], [2, 'd', '25']], '25': [[1, 'd', '10'], [2, 'd', '24'], [2, 'd', '26']],
    '26': [[1, 'd', '11'], [2, 'd', '25']]
}

coordinates = [[161, 99], [234, 214], [482, 155], [588, 152], [749, 147], [112, 404], [276, 404], [70, 563], [271, 564], [564, 390], [543, 555], [751, 464], [125, 18], [378, 18], [614, 18], [750, 18], [380, 220], [74, 300], [375, 300], [480, 300], [535, 300], [378, 410], [371, 554], [73, 660], [379, 660], [550, 660], [750, 660]]

# --- Интерфейс ---
class App:
    def __init__(self, root):
        self.root = root
        self.root.title("Навигатор ООАП")
        
        # Контейнер для настроек
        frame = tk.Frame(root)
        frame.pack(pady=10)

        tk.Label(frame, text="От:").grid(row=0, column=0)
        self.start_cb = ttk.Combobox(frame, values=[i for i in range(1, 13)], width=5)
        self.start_cb.grid(row=0, column=1, padx=5)

        tk.Label(frame, text="До:").grid(row=0, column=2)
        self.end_cb = ttk.Combobox(frame, values=[i for i in range(1, 13)], width=5)
        self.end_cb.grid(row=0, column=3, padx=5)

        tk.Label(frame, text="Транспорт:").grid(row=0, column=4)
        self.type_cb = ttk.Combobox(frame, values=["автомобиль", "велосипед", "пешком"], width=12)
        self.type_cb.grid(row=0, column=5, padx=5)

        self.btn = tk.Button(frame, text="Построить маршрут", command=self.draw_route)
        self.btn.grid(row=0, column=6, padx=10)

        self.time_label = tk.Label(root, text="Предполагаемое время в пути: -", font=("Arial", 12, "bold"))
        self.time_label.pack(pady=5)

        # Холст для карты
        self.canvas = tk.Canvas(root, width=800, height=700)
        self.canvas.pack()

        try:
            self.original_img = Image.open("карта_ООАП.png")
            self.display_image()
        except FileNotFoundError:
            messagebox.showerror("Ошибка", "Файл 'карта_ООАП.png' не найден!")

    def display_image(self, img=None):
        if img is None: img = self.original_img
        self.tk_img = ImageTk.PhotoImage(img)
        self.canvas.create_image(0, 0, anchor="nw", image=self.tk_img)

    def draw_route(self):
        try:
            s = str(int(self.start_cb.get()) - 1)
            f = str(int(self.end_cb.get()) - 1)
            mode = self.type_cb.get()
        except ValueError:
            messagebox.showwarning("Внимание", "Выберите вершины!")
            return

        modes = {"автомобиль": ['d'], "велосипед": ['d', 't'], "пешком": ['d', 't', 'l']}
        road_type = modes.get(mode, ['d', 't', 'l'])

        path, length = dijkstra_with_filter(my_graph, s, f, road_type)

        if not path:
            messagebox.showinfo("Упс", "Путь не найден для данного транспорта")
            return

        # Рисование на копии изображения
        draw_img = self.original_img.copy()
        draw = ImageDraw.Draw(draw_img)
        
        # Отрисовка линий пути
        for i in range(len(path) - 1):
            p1 = coordinates[int(path[i])]
            p2 = coordinates[int(path[i+1])]
            draw.line([tuple(p1), tuple(p2)], fill="red", width=5)
            # Точки узлов
            draw.ellipse([p1[0]-5, p1[1]-5, p1[0]+5, p1[1]+5], fill="blue")
        
        # Конечная точка
        last_p = coordinates[int(path[-1])]
        draw.ellipse([last_p[0]-5, last_p[1]-5, last_p[0]+5, last_p[1]+5], fill="blue")

        self.display_image(draw_img)
        self.time_label.config(text=f"Предполагаемое время в пути: {length/(4-len(road_type))}")

if __name__ == "__main__":
    root = tk.Tk()
    app = App(root)
    root.mainloop()
