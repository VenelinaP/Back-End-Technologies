function solve(startNum, endNum) {
    "use strict"

    let message = "";
    let sum = 0;

    for (let index = startNum; index <= endNum; index += 1) {
        sum += index;
        message += `${index} `;
    }
    console.log(message.trimEnd());
    console.log(`Sum: ${sum}`);    
}
solve(5, 10);