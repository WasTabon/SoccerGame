# Iteration 10 — Финальный polish + UI анимации + переходы

## Что изменилось с предыдущей итерации
- Добавлен `SceneTransition.cs` — плавный fade in/out при переходах между сценами
- Добавлен `MainMenuAnimations.cs` — анимации появления элементов меню (title сверху, кнопки с bounce)
- Добавлен `CountdownUI.cs` — обратный отсчёт "3, 2, 1, GO!" перед стартом матча
- Добавлен `InGameMenuButton.cs` — кнопка MENU во время игры для возврата в главное меню
- Обновлён `Ball.cs` — добавлен флаг autoLaunch, мяч не запускается сам при загрузке через меню
- Обновлён `GameStarter.cs` — использует SceneTransition для fade, запускает countdown перед матчем
- Обновлён `MatchEndUI.cs` — использует SceneTransition, статический метод GoToMenu()
- Обновлён `EndlessMenuButton.cs` — использует общий GoToMenu через SceneTransition
- Обновлён `MainMenuUI.cs` — без изменений логики (GameStarter обрабатывает transition)
- Добавлены editor скрипты для обеих сцен

## Как настроить

### Шаг 1 — Game сцена
1. Открой Game сцену
2. В Unity: меню **SoccerGame → Setup Final Polish - Game (Iteration 10)**
3. Добавятся: TransitionFade, CountdownText, InGameMenuButton

### Шаг 2 — Menu сцена
1. В Unity: меню **SoccerGame → Setup Final Polish - Menu (Iteration 10)**
2. Откроется MainMenu сцена, добавятся: TransitionFade, MainMenuAnimations

## Что создаётся на сценах

### Game сцена
- **SceneTransition** объект + **TransitionFade** Image на GameCanvas (чёрный fade)
- **CountdownUI** объект + **CountdownText** на GameCanvas (большой текст по центру)
- **InGameMenuButton** — маленькая кнопка MENU слева вверху (под ScorePanel)

### Menu сцена
- **SceneTransition** объект + **TransitionFade** Image на MenuCanvas
- **MainMenuAnimations** компонент на MenuRoot со ссылками на title и кнопки

## Как тестировать
1. Запусти с MainMenu сцены
2. Меню появляется с анимациями: title плавно сверху, кнопки с bounce
3. Нажми любой режим → плавный fade to black → Game сцена
4. Game сцена: fade from black → обратный отсчёт "3, 2, 1, GO!" → мяч запускается
5. Во время игры — кнопка MENU слева вверху → fade → возврат в меню
6. Конец матча → панель с RESTART и MENU, обе с fade переходом
7. Endless: кнопка MENU тоже с fade

## Ожидаемый результат
Полностью отполированная игра: плавные переходы между сценами, анимированное меню, обратный отсчёт, кнопка выхода в меню во время игры. Всё ощущается цельно и профессионально.
