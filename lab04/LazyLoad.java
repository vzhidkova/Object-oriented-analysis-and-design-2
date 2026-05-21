package org.example;

import javax.swing.*;
import javax.swing.event.TreeExpansionEvent;
import javax.swing.event.TreeExpansionListener;
import javax.swing.tree.DefaultMutableTreeNode;
import javax.swing.tree.DefaultTreeModel;
import javax.swing.tree.TreePath;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class Main extends JFrame {

    // Класс для хранения минимальной информации об узле в дереве
    static class TaskNode extends DefaultMutableTreeNode {
        private final int id;
        private final String name;
        private boolean isLoaded = false;

        public TaskNode(int id, String name) {
            this.id = id;
            this.name = name;
        }

        public int getId() { return id; }
        public boolean isLoaded() { return isLoaded; }
        public void setLoaded(boolean loaded) { this.isLoaded = loaded; }

        @Override
        public String toString() {
            return name; // JTree использует toString() для отображения имени узла
        }
    }

    private JTree tree;
    private DefaultTreeModel treeModel;
    private DefaultMutableTreeNode virtualRoot;

    private JTextArea infoTextArea;
    private JButton btnCreate;
    private JButton btnDelete;

    private TaskNode selectedNode = null;

    public Main() {
        setTitle("Управление задачами");
        setSize(800, 600);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);

        initDatabase();
        initUI();
        loadRootTasks();
    }

    private void initUI() {
        // Левая часть: Дерево
        virtualRoot = new DefaultMutableTreeNode("Root");
        treeModel = new DefaultTreeModel(virtualRoot);
        tree = new JTree(treeModel);
        tree.setRootVisible(false); // Скрываем виртуальный корень
        tree.setShowsRootHandles(true);

        // Обработка Lazy Loading при разворачивании узла
        tree.addTreeExpansionListener(new TreeExpansionListener() {
            @Override
            public void treeExpanded(TreeExpansionEvent event) {
                TreePath path = event.getPath();
                Object lastComponent = path.getLastPathComponent();
                if (lastComponent instanceof TaskNode) {
                    TaskNode node = (TaskNode) lastComponent;
                    if (!node.isLoaded()) {
                        loadChildren(node);
                    }
                }
            }
            @Override
            public void treeCollapsed(TreeExpansionEvent event) {}
        });

        // Обработка кликов: ленивая загрузка Info + снятие выделения при повторном клике
        tree.addMouseListener(new MouseAdapter() {
            @Override
            public void mousePressed(MouseEvent e) {
                TreePath path = tree.getPathForLocation(e.getX(), e.getY());

                if (path == null) {
                    clearSelection();
                    return;
                }

                Object component = path.getLastPathComponent();
                if (component instanceof TaskNode) {
                    TaskNode clickedNode = (TaskNode) component;

                    // Если узел уже был выбран — сворачиваем его и снимаем выделение
                    if (tree.isPathSelected(path) && selectedNode == clickedNode) {
                        SwingUtilities.invokeLater(() -> {
                            tree.clearSelection();
                            tree.collapsePath(path);
                            clearSelection();
                        });
                    } else {
                        // Обычный выбор узла — загружаем Info из БД
                        selectedNode = clickedNode;
                        btnDelete.setEnabled(true);
                        loadTaskInfo(selectedNode.getId());
                    }
                }
            }
        });

        // Правая часть: Информация и кнопки
        JPanel rightPanel = new JPanel(new BorderLayout());
        infoTextArea = new JTextArea();
        infoTextArea.setEditable(false);
        infoTextArea.setBorder(BorderFactory.createTitledBorder("Информация о задаче"));

        JPanel buttonPanel = new JPanel(new FlowLayout(FlowLayout.RIGHT));
        btnCreate = new JButton("Создать задачу");
        btnDelete = new JButton("Удалить задачу");
        btnDelete.setEnabled(false);

        buttonPanel.add(btnCreate);
        buttonPanel.add(btnDelete);
        rightPanel.add(new JScrollPane(infoTextArea), BorderLayout.CENTER);
        rightPanel.add(buttonPanel, BorderLayout.SOUTH);

        // Слушатели кнопок
        btnCreate.addActionListener(e -> onCreateTask());
        btnDelete.addActionListener(e -> onDeleteTask());

        // Компоновка
        JSplitPane splitPane = new JSplitPane(JSplitPane.HORIZONTAL_SPLIT, new JScrollPane(tree), rightPanel);
        splitPane.setDividerLocation(300);
        add(splitPane);
    }

    private void clearSelection() {
        selectedNode = null;
        infoTextArea.setText("");
        btnDelete.setEnabled(false);
    }

    // --- Блок работы с БД (JDBC) ---

    private Connection getConnection() throws SQLException {
        return DriverManager.getConnection("jdbc:h2:mem:taskdb;DB_CLOSE_DELAY=-1");
    }

    private void initDatabase() {
        try (Connection conn = getConnection(); Statement stmt = conn.createStatement()) {
            stmt.execute("CREATE TABLE IF NOT EXISTS tasks (" +
                    "id INT GENERATED BY DEFAULT AS IDENTITY PRIMARY KEY," +
                    "parent_id INT," +
                    "name VARCHAR(255) NOT NULL," +
                    "info CLOB)"); // Можно использовать VARCHAR(MAX) или CLOB для больших текстов
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    // Загрузка корневых задач (только ID и Name)
    private void loadRootTasks() {
        virtualRoot.removeAllChildren();
        String sql = "SELECT id, name FROM tasks WHERE parent_id IS NULL";
        try (Connection conn = getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {

            while (rs.next()) {
                TaskNode rootNode = new TaskNode(rs.getInt("id"), rs.getString("name"));
                // Добавляем пустышку, чтобы узел стал "разворачиваемым" (+ icon)
                rootNode.add(new DefaultMutableTreeNode("Loading..."));
                virtualRoot.add(rootNode);
            }
            treeModel.reload();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    // Lazy Loading: Загрузка подзадач при разворачивании
    private void loadChildren(TaskNode parentNode) {
        parentNode.removeAllChildren(); // Удаляем "Loading..."

        String sql = "SELECT id, name FROM tasks WHERE parent_id = ?";
        try (Connection conn = getConnection();
             PreparedStatement pstmt = conn.prepareStatement(sql)) {

            pstmt.setInt(1, parentNode.getId());
            try (ResultSet rs = pstmt.executeQuery()) {
                while (rs.next()) {
                    TaskNode childNode = new TaskNode(rs.getInt("id"), rs.getString("name"));
                    childNode.add(new DefaultMutableTreeNode("Loading..."));
                    parentNode.add(childNode);
                }
            }
            parentNode.setLoaded(true);
            treeModel.nodeStructureChanged(parentNode);
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    // Lazy Loading: Загрузка поля Info при клике
    private void loadTaskInfo(int taskId) {
        String sql = "SELECT info FROM tasks WHERE id = ?";
        try (Connection conn = getConnection();
             PreparedStatement pstmt = conn.prepareStatement(sql)) {

            pstmt.setInt(1, taskId);
            try (ResultSet rs = pstmt.executeQuery()) {
                if (rs.next()) {
                    infoTextArea.setText(rs.getString("info"));
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    // Создание задачи / подзадачи
    private void onCreateTask() {
        String name = JOptionPane.showInputDialog(this, "Введите название задачи:");
        if (name == null || name.trim().isEmpty()) return;

        String info = JOptionPane.showInputDialog(this, "Введите описание задачи (Info):");

        String sql = "INSERT INTO tasks (parent_id, name, info) VALUES (?, ?, ?)";
        try (Connection conn = getConnection();
             PreparedStatement pstmt = conn.prepareStatement(sql, Statement.RETURN_GENERATED_KEYS)) {

            if (selectedNode != null) {
                pstmt.setInt(1, selectedNode.getId()); // Создаем подзадачу
            } else {
                pstmt.setNull(1, Types.INTEGER); // Создаем корневую задачу
            }
            pstmt.setString(2, name);
            pstmt.setString(3, info);
            pstmt.executeUpdate();

            // Обновляем граф
            if (selectedNode != null) {
                // Перезагружаем ветку родителя
                selectedNode.setLoaded(false);
                tree.expandPath(new TreePath(selectedNode.getPath()));
                loadChildren(selectedNode);
            } else {
                loadRootTasks(); // Перезагружаем корни
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    // Удаление задачи и всех ее потомков
    private void onDeleteTask() {
        if (selectedNode == null) return;

        int confirm = JOptionPane.showConfirmDialog(this,
                "Вы уверены, что хотите удалить задачу и все её подзадачи?",
                "Удаление", JOptionPane.YES_NO_OPTION);

        if (confirm != JOptionPane.YES_OPTION) return;

        try (Connection conn = getConnection()) {
            conn.setAutoCommit(false); // Включаем транзакцию для безопасности
            try {
                deleteTaskRecursive(conn, selectedNode.getId());
                conn.commit();

                // Обновляем UI
                DefaultMutableTreeNode parent = (DefaultMutableTreeNode) selectedNode.getParent();
                selectedNode.removeFromParent();
                treeModel.nodeStructureChanged(parent);
                clearSelection();
            } catch (SQLException ex) {
                conn.rollback();
                throw ex;
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    // Рекурсивный метод каскадного удаления из БД
    private void deleteTaskRecursive(Connection conn, int id) throws SQLException {
        // Сначала ищем всех детей
        List<Integer> childIds = new ArrayList<>();
        String selectSQL = "SELECT id FROM tasks WHERE parent_id = ?";
        try (PreparedStatement pstmt = conn.prepareStatement(selectSQL)) {
            pstmt.setInt(1, id);
            try (ResultSet rs = pstmt.executeQuery()) {
                while (rs.next()) {
                    childIds.add(rs.getInt("id"));
                }
            }
        }

        // Рекурсивно удаляем детей
        for (int childId : childIds) {
            deleteTaskRecursive(conn, childId);
        }

        // Удаляем саму задачу
        String deleteSQL = "DELETE FROM tasks WHERE id = ?";
        try (PreparedStatement pstmt = conn.prepareStatement(deleteSQL)) {
            pstmt.setInt(1, id);
            pstmt.executeUpdate();
        }
    }

    public static void main(String[] args) {
        // Устанавливаем системный стиль отображения окон
        try { UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName()); } catch (Exception ignored) {}

        SwingUtilities.invokeLater(() -> {
            new Main().setVisible(true);
        });

    }
}
