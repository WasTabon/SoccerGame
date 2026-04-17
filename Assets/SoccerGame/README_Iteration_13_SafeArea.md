# Iteration 13 — Safe Area (чёлка/нотч)

## Что изменилось с предыдущей итерации
- Добавлен `SafeArea.cs` — подстраивает RectTransform UI под Screen.safeArea
- Добавлен `CameraSafeArea.cs` — подстраивает Camera.rect под Screen.safeArea (игровое поле не залезает за чёлку)
- Обновлён `GameStarter.cs` — рекурсивный поиск UI панелей (работает после обёртки в SafeAreaPanel)
- Добавлен editor скрипт `SoccerGameSetup_Iteration13.cs` — две кнопки для Game и Menu сцен

## Как настроить

### Шаг 1 — Game сцена
1. Открой Game сцену
2. В Unity: меню **SoccerGame → Setup Safe Area - Game (Iteration 13)**
3. Камера получит CameraSafeArea, все дети GameCanvas обернутся в SafeAreaPanel

### Шаг 2 — Menu сцена
1. В Unity: меню **SoccerGame → Setup Safe Area - Menu (Iteration 13)**
2. Все дети MenuCanvas обернутся в SafeAreaPanel

## Что создаётся/меняется

### Game сцена
- **CameraSafeArea** компонент на Main Camera — viewport подстраивается под safe area
- **SafeAreaPanel** внутри GameCanvas — все UI элементы перемещены внутрь, RectTransform подстраивается

### Menu сцена
- **SafeAreaPanel** внутри MenuCanvas — все UI элементы перемещены внутрь

## Как тестировать
1. В Unity: Edit → Project Settings → Player → Resolution → выбери устройство с чёлкой (например iPhone 14)
2. Или используй Device Simulator
3. Игровое поле и UI не должны залезать за область чёлки
4. Чёрная полоса в зоне нотча — нормально

## Ожидаемый результат
На устройствах с чёлкой/нотчем игровое поле и весь UI находятся в безопасной зоне. Вратарь, ворота и счёт полностью видны.
