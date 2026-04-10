# Iteration 8 — Главное меню + выбор режима

## Что изменилось с предыдущей итерации
- Добавлен `GameStarter.cs` — синглтон DontDestroyOnLoad, хранит выбранный режим, настраивает Game сцену
- Добавлен `MainMenuUI.cs` — кнопки выбора режима на меню
- Добавлен `EndlessMenuButton.cs` — кнопка возврата в меню из Endless
- Обновлён `MatchEndUI.cs` — добавлена кнопка MENU для возврата в главное меню
- Добавлены editor скрипты: `SoccerGameSetup_Iteration8_Menu.cs` и `SoccerGameSetup_Iteration8_Game.cs`

## Как настроить

### Шаг 1 — Сохранить игровую сцену как Game
1. Открой текущую игровую сцену
2. В Unity: меню **SoccerGame → Setup Game Scene (Iteration 8)**
3. Сцена сохранится как `Assets/SoccerGame/Scenes/Game.unity`
4. Добавится кнопка MENU на панели конца матча и в Endless UI

### Шаг 2 — Создать сцену MainMenu
1. В Unity: меню **SoccerGame → Setup MainMenu Scene (Iteration 8)**
2. Создастся сцена `Assets/SoccerGame/Scenes/MainMenu.unity` с меню
3. Обе сцены автоматически добавятся в Build Settings

### Шаг 3 — Проверить Build Settings
1. File → Build Settings
2. Убедись что MainMenu идёт первой (index 0), Game — второй (index 1)
3. Если порядок неверный — перетащи

## Что создаётся
### MainMenu сцена
- **MenuCanvas** — Canvas с кнопками
- **Title** — "SOCCER GAME"
- **MatchButton** — зелёная кнопка "MATCH MODE"
- **EndlessButton** — синяя кнопка "ENDLESS MODE"
- **MenuRoot** — компонент MainMenuUI со ссылками

### Game сцена (дополнения)
- **MenuButton** в MatchEndPanel — серая кнопка "MENU"
- **EndlessMenuButton** в EndlessPanel — кнопка "MENU"

## Как тестировать
1. Запусти с MainMenu сцены
2. Нажми MATCH MODE → загрузится Game с матчем до 5, ScorePanel активен
3. Доиграй матч → появится панель с RESTART и MENU
4. MENU → возврат в MainMenu
5. Нажми ENDLESS MODE → загрузится Game с Endless, EndlessPanel активен
6. В Endless есть кнопка MENU для возврата

## Ожидаемый результат
Полный флоу: MainMenu → выбор режима → Game → результат → рестарт или возврат в меню.
