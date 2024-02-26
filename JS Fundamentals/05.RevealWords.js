function solve(words, template) {
    "use strict"

    const separetedWords = words.split(', ')
    
    for (const separetedWord of separetedWords) {
        template = template.replace("*".repeat(separetedWord.length), separetedWord)
       
    }    
    console.log(template);
}
solve('great, learning', 'softuni is ***** place for ******** new programming languages');