document.getElementById("calcTotal").addEventListener("click", function(){
    // get inputs: base pizza cost and extras choices
    let basePizza = parseFloat(document.getElementById("cboPizza").value);
    let extras = document.querySelectorAll("input[name=chkOptions]:checked");
    let extrasCost = 0;

    for(let e of extras){
        extrasCost += parseFloat(e.value);
    }

    let total = basePizza + extrasCost;
    document.getElementById("totalAmount").textContent = "Total Amount: $" + total.toFixed(2);
});