Инструкции по использованию
Скачивание Docker-образа Для загрузки Docker-образа выполните следующую команду:
docker pull razackoff/hotel-booking-service:v1.0

Запуск контейнера Запустите контейнер с помощью следующей команды:
docker run -d -p 8080:8080 --name hotel-booking-service razackoff/hotel-booking-service:v1.0

Доступ к веб-интерфейсу После запуска контейнера веб-интерфейс будет доступен по адресу:
http://localhost:8080
