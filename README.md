# Algoritmo "Depredador y Presa"
Este proyecto implementa una simulación del algoritmo "Depredador y Presa", también conocido como modelo Lotka-Volterra. Este algoritmo se utiliza para modelar la interacción entre dos poblaciones, una de depredadores y otra de presas.

## Requisitos
Para ejecutar la simulación, se necesitan los siguientes requisitos:

Visual Studio
.NET Framework instalada

## Descripción del algoritmo
El algoritmo "Depredador y Presa" se basa en un conjunto de ecuaciones diferenciales que modelan la evolución de dos poblaciones: una población de depredadores y una población de presas. La ecuación que modela la evolución de la población de presas es la siguiente:

$$dP/dt = aP - bPD$$

Donde:

$P$ es la población de presas

$t$ es el tiempo

$a$ es la tasa de crecimiento de las presas

$b$ es la tasa de mortalidad de las presas debido a los depredadores

$D$ es la población de depredadores

La ecuación que modela la evolución de la población de depredadores es la siguiente:

$$dD/dt = cbPD - dD$$

Donde:

D es la población de depredadores
t es el tiempo
c es la eficacia de la conversión de presas en depredadores
b es la tasa de mortalidad de las presas debido a los depredadores
P es la población de presas
d es la tasa de mortalidad de los depredadores
## Referencias
- Lotka, A. J. (1925). Elements of physical biology. Science Press.

- Volterra, V. (1926). Variazioni e fluttuazioni del numero d'individui in specie animali conviventi. Mem. R. Accad. Naz. dei Lincei, 2(6), 31-113.

