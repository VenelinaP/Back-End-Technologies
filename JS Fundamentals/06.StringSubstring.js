function solve(wordTosearch, textForSearching) {
    "use strict";

    const hasWord = textForSearching
        .toLowerCase()
        .split(' ')
        .includes(wordTosearch)

    if(hasWord) {
        console.log(wordTosearch)
    } else {
        console.log(`${wordTosearch} not found!`)
    }
}
solve('javascript', 'JavaScript is the best programming language');