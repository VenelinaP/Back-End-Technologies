function shop(products) {

    const numOfProducts = parseInt(products[0], 10);
    const allProducts = products.slice(1, numOfProducts + 1)
    const allCommands = products.slice(numOfProducts + 1)

    for (const rawCommand of allCommands) {
        const commandComponents = rawCommand.split(' ')
        const command = commandComponents[0]

        if (command === 'Sell'){

            const firstProduct = allProducts.shift()
            console.log(`${firstProduct} product sold!`)

        } else if (command === 'Add'){
            const nameOfProduct = rawCommand.slice(4)
            allProducts.push(nameOfProduct)
           
        } else if (command === 'Swap') {
            const startIndex = parseInt(commandComponents[1], 10)
            const endIndex = parseInt(commandComponents[2], 10)

            if (isNaN(startIndex) || startIndex < 0 || startIndex > allProducts.length) {
                continue
            }
            if (isNaN(endIndex) || endIndex < 0 || endIndex > allProducts.length) {
                continue
            }
            const startIndexValue = allProducts[startIndex]
            allProducts[startIndex] = allProducts[endIndex]
            allProducts[endIndex] = startIndexValue

            console.log("Swapped!")

        } else if (command === 'End') {
            break
        }        
    }

    if (allProducts.length) {
        console.log(`Products left: ${allProducts.join(', ')}`)
    } else {
        console.log("The shop is empty")
    }
}
//shop(['3', 'Apple', 'Banana', 'Orange','Sell', 'End', 'Swap 0 1'])
//shop(['5', 'Milk', 'Eggs', 'Bread','Cheese', 'Butter', 'Add Yogurt', 'Swap 14', 'End'])
shop(['3', 'Shampoo', 'Soap', 'Toothpaste', 'Sell', 'Sell', 'Sell', 'End'])