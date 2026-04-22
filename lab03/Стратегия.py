import tkinter as tk
from tkinter import ttk, messagebox
from PIL import Image, ImageTk, ImageDraw
import heapq
from abc import ABC, abstractmethod

class TravelStrategy(ABC):
    @abstractmethod
    def get_road_types(self):
        pass
    @abstractmethod
    def get_speed(self):
        pass

class CarStrategy(TravelStrategy):
    def get_road_types(self):
        return ['d']
    def get_speed(self): return 3

class BicycleStrategy(TravelStrategy):
    def get_road_types(self):
        return ['d', 't']
    def get_speed(self): return 2

class WalkStrategy(TravelStrategy):
    def get_road_types(self):
        return ['d', 't', 'l']
    def get_speed(self): return 1

class Navigator:
    def __init__(self):
        self._strategy = None

    def set_strategy(self, strategy: TravelStrategy):
        self._strategy = strategy

    def calculate(self, graph, start, end):
        if not self._strategy:
            return [], 0
        
        path, length = self._dijkstra(graph, start, end, self._strategy.get_road_types())
        
        if length == float('inf'):
            return [], 0
            
        time = round(length / self._strategy.get_speed(), 2)
        return path, time

    def _dijkstra(self, graph, start_node, end_node, allowed_types):
        distances = {node: float('inf') for node in graph}
        distances[start_node] = 0
        predecessors = {node: None for node in graph}
        priority_queue = [(0, start_node)]
        
        while priority_queue:
            current_distance, u = heapq.heappop(priority_queue)
            if current_distance > distances[u]: continue
            if u == end_node: break
            
            for weight, path_type, v in graph.get(u, []):
                if path_type in allowed_types:
                    distance = current_distance + weight
                    if distance < distances[v]:
                        distances[v] = distance
                        predecessors[v] = u
                        heapq.heappush(priority_queue, (distance, v))
        
        path = []
        current = end_node
        if distances[end_node] == float('inf'): return [], float('inf')
        while current is not None:
            path.append(current)
            current = predecessors[current]
        return path[::-1], distances[end_node]


MY_GRAPH = {
    '0': [[2, 'l', '1'], [1, 'd', '12']],
    '1': [[1, 'l', '0'], [3, 'l', '6'], [1, 'd', '12']],
    '2': [[1, 't', '3'], [1, 'd', '19']],
    '3': [[1, 't', '2'], [1, 't', '4'], [2, 'l', '9'], [1, 'd', '14']],
    '4': [[1, 't', '3'], [1, 'd', '15']],
    '5': [[1, 't', '6'], [1, 't', '7'], [1, 'd', '17']],
    '6': [[3, 'l', '1'], [1, 't', '5'], [1, 't', '8'], [1, 'd', '21']],
    '7': [[1, 't', '5'], [1, 't', '8'], [1, 'd', '23']],
    '8': [[1, 't', '6'], [1, 't', '7'], [2, 't', '10'], [1, 'd', '22']],
    '9': [[2, 'l', '3'], [1, 't', '11'], [1, 'd', '20']],
    '10': [[2, 't', '8'], [1, 't', '11']],
    '11': [[1, 't', '9'], [1, 't', '10'], [1, 'd', '26']],
    '12': [[1, 'd', '0'], [3, 'd', '13']],
    '13': [[3, 'd', '12'], [3, 'd', '14'], [3, 'd', '16']],
    '14': [[1, 'd', '3'], [3, 'd', '13'], [2, 'd', '15']],
    '15': [[1, 'd', '4'], [2, 'd', '14']],
    '16': [[1, 'd', '1'], [3, 'd', '13'], [1, 'd', '18']],
    '17': [[1, 'd', '5'], [4, 'd', '18']],
    '18': [[1, 'd', '16'], [4, 'd', '17'], [1, 'd', '19'], [1, 'd', '21']],
    '19': [[1, 'd', '2'], [1, 'd', '18'], [1, 'd', '20']],
    '20': [[1, 'd', '9'], [1, 'd', '19']],
    '21': [[1, 'd', '6'], [1, 'd', '18'], [2, 'd', '22']],
    '22': [[1, 'd', '8'], [2, 'd', '21'], [1, 'd', '24']],
    '23': [[1, 'd', '7'], [4, 'd', '24']],
    '24': [[1, 'd', '22'], [4, 'd', '23'], [2, 'd', '25']],
    '25': [[1, 'd', '10'], [2, 'd', '24'], [2, 'd', '26']],
    '26': [[1, 'd', '11'], [2, 'd', '25']]
}

COORDINATES = [[161, 99], [234, 214], [482, 155], [588, 152], [749, 147], [112, 404], [276, 404], [70, 563], [271, 564], [564, 390], [543, 555], [751, 464], [125, 18], [378, 18], [614, 18], [750, 18], [380, 220], [74, 300], [375, 300], [480, 300], [535, 300], [378, 410], [371, 554], [73, 660], [379, 660], [550, 660], [750, 660]]

class App:
    def __init__(self, root):
        self.root = root
        self.root.title("Навигатор с паттерном Стратегия")
        self.navigator = Navigator()
        
        # Интерфейс
        controls = tk.Frame(root)
        controls.pack(pady=10)

        self.start_node = ttk.Combobox(controls, values=[i for i in range(1, 13)], width=5)
        self.end_node = ttk.Combobox(controls, values=[i for i in range(1, 13)], width=5)
        self.mode_var = ttk.Combobox(controls, values=["автомобиль", "велосипед", "пешком"], width=12)
        
        for i, widget in enumerate([tk.Label(controls, text="От:"), self.start_node, 
                                    tk.Label(controls, text="До:"), self.end_node,
                                    tk.Label(controls, text="Тип:"), self.mode_var]):
            widget.grid(row=0, column=i, padx=5)

        tk.Button(controls, text="Построить маршрут", command=self.run).grid(row=0, column=6, padx=10)
        
        self.res_label = tk.Label(root, text="Предполагаемое время в пути: -", font=("Arial", 10, "bold"))
        self.res_label.pack()

        self.canvas = tk.Canvas(root, width=850, height=700)
        self.canvas.pack()

        try:
            self.img = Image.open("карта_ООАП.png")
            self.render_map()
        except:
            messagebox.showerror("Ошибка", "Файл карта_ООАП.png не найден!")

    def render_map(self, current_img=None):
        photo = ImageTk.PhotoImage(current_img if current_img else self.img)
        self.canvas.image = photo
        self.canvas.create_image(0, 0, anchor="nw", image=photo)

    def run(self):
        try:
            s, f = str(int(self.start_node.get())-1), str(int(self.end_node.get())-1)
            mode = self.mode_var.get()
        except:
            return messagebox.showwarning("!", "Выберите пункты и тип транспорта")

        strategies = {
            "автомобиль": CarStrategy(),
            "велосипед": BicycleStrategy(),
            "пешком": WalkStrategy()
        }
        
        strategy = strategies.get(mode)
        if not strategy: return
        
        self.navigator.set_strategy(strategy)
        path, travel_time = self.navigator.calculate(MY_GRAPH, s, f)

        if not path:
            return messagebox.showinfo("Инфо", "Путь невозможен")

        # Отрисовка
        draw_img = self.img.copy()
        draw = ImageDraw.Draw(draw_img)
        for i in range(len(path)-1):
            p1, p2 = COORDINATES[int(path[i])], COORDINATES[int(path[i+1])]
            draw.line([tuple(p1), tuple(p2)], fill="red", width=4)
            draw.ellipse([p1[0]-4, p1[1]-4, p1[0]+4, p1[1]+4], fill="blue")
            
        self.render_map(draw_img)
        
        # Вывод результата
        self.res_label.config(
            text=f"Скорость: {strategy.get_speed()} ед/мин | Время в пути: {travel_time} мин."
        )

if __name__ == "__main__":
    root = tk.Tk()
    app = App(root)
    root.mainloop()
