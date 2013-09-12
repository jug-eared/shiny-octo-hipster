for i in range(10):
    print(i)

f = lambda x: x**2
L = list(range(10))
L = list(map(f, L))