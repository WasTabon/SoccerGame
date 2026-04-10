# Iteration 7 — Рост сложности + Endless Mode

## Что изменилось с предыдущей итерации
- Обновлён `MatchManager.cs` — добавлен enum GameMode (Match/Endless), streak, bestScore, SetGameMode()
- Добавлен `DifficultyManager.cs` — плавно увеличивает скорость мяча и вратаря со временем
- Добавлен `EndlessUI.cs` — UI для Endless режима (счёт, streak, best)
- Добавлен editor скрипт `SoccerGameSetup_Iteration7.cs`
- Остальные скрипты без изменений

## Как настроить
1. Скопируй файлы в проект (заменить MatchManager.cs)
2. В Unity: меню **SoccerGame → Setup Difficulty and Endless (Iteration 7)**
3. DifficultyManager создастся с ссылками на Ball и AIGoalkeeper
4. EndlessPanel добавится на GameCanvas (скрыт по умолчанию)

## Как переключить режим (временно через Inspector)
1. Выбери MatchManager на сцене
2. В Inspector поменяй Game Mode на Endless
3. EndlessPanel нужно вручную включить (SetActive true), ScorePanel — выключить
4. В итерации 8 (меню) это будет автоматически

## Как тестировать
### Match Mode (по умолчанию)
1. Играй как раньше — матч до 5 голов
2. Со временем мяч и вратарь ускоряются
3. При рестарте сложность сбрасывается

### Endless Mode
1. Переключи на Endless в Inspector
2. Забитый гол = очки (1 + streak/3 бонус)
3. Пропущенный гол = streak сбрасывается
4. Best score сохраняется между сессиями (PlayerPrefs)

## Ожидаемый результат
Сложность плавно растёт со временем. Endless режим работает — бесконечная игра на очки со streak-бонусами и сохранением лучшего результата.
