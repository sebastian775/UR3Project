import time

stop = 0


def final():
    global stop 
    stop = time.time

if __name__ == '__main__':
    start = time.time

    while(stop>=0):
        i=i+1
        print("r")

    tiempo = start-stop
    print(tiempo)