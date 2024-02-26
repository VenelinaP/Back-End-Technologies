function solve(inputArr) {
    "use strict";

    inputArr.sort((a, b) => a - b);

    const result = [];

    while (inputArr.length > 0)
    {
        const firstElement = inputArr.shift()
        const lastElement = inputArr.pop()

        result.push(firstElement)
        if (lastElement !== undefined) 
        {
            result.push(lastElement)
        }
    }
    return result
}
console.log(solve([1, 65, 3, 52, 48, 63, 31, -3, 18, 56]));