function solve(firsNum, secondNum, thitdNum) {
    "use strict";

    const sum = (first, second) => first + second
    const substract = (first, second) => first - second

    const result = substract(sum(firsNum, secondNum), thitdNum)

    console.log(result);
}
solve(23, 6, 10);