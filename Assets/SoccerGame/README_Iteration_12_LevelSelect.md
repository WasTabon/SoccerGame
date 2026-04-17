# Iteration 12 — UI выбора уровней + кнопка LEVELS в меню

## Что изменилось с предыдущей итерации
- Добавлен `LevelSelectButton.cs` — компонент кнопки уровня (номер, голы, замок)
- Добавлен `LevelSelectUI.cs` — прокручиваемый список 30 уровней из префабов
- Обновлён `MainMenuUI.cs` — добавлена кнопка LEVELS и ссылка на LevelSelectUI
- Обновлён `MainMenuAnimations.cs` — анимация кнопки LEVELS
- Добавлен editor скрипт `SoccerGameSetup_Iteration12_Menu.cs`
- Создаётся **префаб** `Assets/SoccerGame/Prefabs/LevelButton.prefab` — для замены арта

## Как настроить
1. Скопируй файлы в проект (заменить MainMenuUI.cs, MainMenuAnimations.cs)
2. В Unity: меню **SoccerGame → Setup Level Select - Menu (Iteration 12)**
3. Откроется MainMenu сцена, создадутся: кнопка LEVELS, LevelSelectPanel, префаб LevelButton

## Что создаётся

### Префаб
- `Assets/SoccerGame/Prefabs/LevelButton.prefab` — кнопка уровня, можно заменить арт:
  - **LevelNumber** (TextMeshProUGUI) — номер уровня
  - **GoalsInfo** (TextMeshProUGUI) — количество голов
  - **LockIcon** (TextMeshProUGUI + Image) — показывается для закрытых уровней
  - **Background** (Image) — фон кнопки

### MainMenu сцена
- **LevelsButton** — оранжевая кнопка "LEVELS" на главном экране
- **LevelSelectPanel** (скрыт) — полноэкранная панель:
  - **Header** — "SELECT LEVEL"
  - **ScrollArea** — прокрутка с GridLayout (3 колонки)
  - **Content** — контейнер для кнопок (заполняется из префаба в runtime)
  - **BackButton** — возврат в главное меню

## Как тестировать
1. Запусти с MainMenu
2. Три кнопки: MATCH MODE, ENDLESS MODE, LEVELS (с анимацией появления)
3. Нажми LEVELS → появится список уровней
4. Level 1 открыт (зелёный), остальные заблокированы (серые, "LOCKED")
5. Тап на Level 1 → fade → Game сцена в режиме Levels
6. Забей нужное кол-во голов → LEVEL COMPLETE → NEXT LEVEL
7. Следующий уровень разблокирован
8. Вернись в меню → LEVELS → Level 2 теперь открыт
9. BACK → возврат на главный экран меню

## Как поменять арт кнопки уровня
1. Открой `Assets/SoccerGame/Prefabs/LevelButton.prefab`
2. Замени спрайты, цвета, шрифты
3. Все 30 кнопок автоматически обновятся (инстансятся из префаба)

## Ожидаемый результат
Полный флоу режима уровней: меню → LEVELS → список с замками → тап → игра → результат → next/retry/menu. Прогресс сохраняется.
