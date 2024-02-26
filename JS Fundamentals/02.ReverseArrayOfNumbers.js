function solve(n, input) {

    let reverseArr = [];

    for (let i =0; i < n; i++)
    {
        reverseArr.unshift(input[i]);
    }
    console.log(reverseArr.join(" "));
}

solve(3, [10, 20, 30, 40, 50]);