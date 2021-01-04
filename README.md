# YmlParser
Программа принимает на вход одну из двух приведённых далее команд.
# Команда save <id магазина> <ссылка на yml-файл>
Сохраняет в базу данных информацию о товарах из указанного файла, присваивая всем единицам указанный id магазина
# Команда print <id магазина>
Выводит на консоль все товарные единицы для магазина с указанным id
# О программе
В программе применяется инверсия зависимостей для уменьшения связанности. Этот принцип уже позволил очень легко заменить реализацию парсинга файла: в пространстве имен YmlParser.Parser есть оба его варианта, старый и новый, XDocYmlParser и StreamParser соответственно. Последний, по результатам профилирования, помог сократить потребление памяти при парсинге документа почти вдвое, что особенно существенно, когда происходит работа с крупными файлами.
