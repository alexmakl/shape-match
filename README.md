# shape-match
Shape Match Test Task - Unity Developer

Задача

● Реализовать базовый прототип мини-игры по представленному макету и описанию;

● Платформа: Unity (Android или iOS, на ваш выбор);

● Нет необходимости в сейвах, меню или сложной анимации;

● Графика из макета представлена в ознакомительных целях;

● Используйте любые собственные ассеты.

Видео геймплея https://drive.google.com/file/d/1mpnz18iGRnQBgvMlIvGxf_yIPq5UWHLb/view?usp=sharing

Небольшой коментарий по огранизации кода. Сейчас специальные типы фигурок захардкожены. По-хорошему все настройки и переменные конкретного уровня нужно вынести отдельно и задавать в LevelManager, чтобы TileSpawner не содержал логику, а отвечал только за спавн фигурок.

И по мере роста проекта необходимо будет выделять новые сущности, чтобы соблюдать принцыпы SOLID и архитектурные паттерны.
