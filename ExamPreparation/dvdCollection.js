function dvdCollection(dvds) {

    const numOfDvds = parseInt(dvds[0], 10);
    const allDvds = dvds.slice(1, numOfDvds + 1)
    const allCommands = dvds.slice(numOfDvds + 1)

    for (const rawCommand of allCommands) {
        const commandComponents = rawCommand.split(' ')
        const command = commandComponents[0]

        if (command === "Watch" ){

            const dvd1 = allDvds.shift()
            console.log(`${dvd1} DVD watched!`)

        } else if (command === 'Buy'){
            const nameOfDvds = rawCommand.slice(4)
            allDvds.push(nameOfDvds)
           
        } else if (command === 'Swap') {
            const startIndex = parseInt(commandComponents[1], 10)
            const endIndex = parseInt(commandComponents[2], 10)

            if (isNaN(startIndex) || startIndex < 0 || startIndex > allDvds.length) {
                continue
            }
            if (isNaN(endIndex) || endIndex < 0 || endIndex > allDvds.length) {
                continue
            }
            const startIndexValue = allDvds[startIndex]
            allDvds[startIndex] = allDvds[endIndex]
            allDvds[endIndex] = startIndexValue

            console.log("Swapped!")

        } else if (command === "Done") {
            break
        }        
    }

    if (allDvds.length) {
        console.log(`DVDs left: ${allDvds.join(', ')}`)
    } else {
        console.log("The collection is empty")
    }
}
//dvdCollection(['3', 'The Matrix', 'The Godfather', 'The Shawshank Redemption', 'Watch', 'Done', 'Swap 0 1'])
dvdCollection(['5', 'The Lion King', 'Frozen', 'Moana', 'Toy Story', 'Shrek', 'Buy Coco', 'Swap 2 4', 'Done'])