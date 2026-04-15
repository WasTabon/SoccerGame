# Iteration 9 — Game Feel + Polish (DOTween)

## Что изменилось с предыдущей итерации
- Добавлен `ScreenShake.cs` — тряска камеры при ударах (light) и голах (heavy)
- Добавлен `BallEffects.cs` — squash & stretch при ударе, вспышка при голе
- Добавлен `GoalEffect.cs` — flash экрана + slowmo при голе
- Обновлён `Flipper.cs` — вызывает эффекты при ударе мяча (squash, shake, мигание цвета)
- Обновлён `MatchEndUI.cs` — DOTween анимации появления/исчезновения панели, ссылка на contentRect
- Обновлён `ScoreUI.cs` — punch scale при изменении счёта
- Обновлён `EndlessUI.cs` — punch scale счёта, bounce анимация streak
- Добавлен editor скрипт `SoccerGameSetup_Iteration9.cs`

## Как настроить
1. Скопируй файлы в проект (заменить Flipper.cs, MatchEndUI.cs, ScoreUI.cs, EndlessUI.cs)
2. Открой Game сцену
3. В Unity: меню **SoccerGame → Setup Game Feel (Iteration 9)**
4. Компоненты эффектов добавятся на существующие объекты

## Что создаётся/добавляется на сцене
- **ScreenShake** на Main Camera
- **BallEffects** + **TrailRenderer** на Ball
- **GoalEffect** объект + **FlashImage** на GameCanvas (полноэкранный Image для flash)
- **contentRect** ссылка в MatchEndUI для анимации панели

## Как тестировать
1. Нажми Play
2. Удар флиппером → мяч растягивается (squash & stretch), камера трясётся слегка, флиппер мигает белым
3. Мяч оставляет trail за собой
4. Гол → flash экрана (зелёный = забил, красный = пропустил), сильная тряска камеры, slowmo на полсекунды
5. Счёт при изменении — punch scale анимация
6. Панель конца матча появляется с bounce анимацией (scale from 0)
7. Рестарт — панель уменьшается и исчезает

## Ожидаемый результат
Игра ощущается сочно и отзывчиво. Каждый удар и гол имеют визуальный и тактильный feedback.
