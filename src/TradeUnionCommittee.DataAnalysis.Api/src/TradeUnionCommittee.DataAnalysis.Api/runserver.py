from os import environ
from Controllers import app

if __name__ == '__main__':
    HOST = environ.get('SERVER_HOST', '0.0.0.0')
    try:
        PORT = 5000
    except ValueError:
        PORT = 5000
    app.run(HOST, PORT)