from os import environ
from controllers import app

if __name__ == '__main__':
    HOST = environ.get('SERVER_HOST', 'localhost')
    try:
        PORT = 8700
    except ValueError:
        PORT = 8700
    app.run(HOST, PORT)