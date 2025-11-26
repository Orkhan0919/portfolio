let sum = document.querySelector(".btnPlus");
let minus = document.querySelector(".btnMinus");
let mult = document.querySelector(".btnMult");
let dvd = document.querySelector(".btnDvd");
let input1 = document.querySelector(".input1");
let input2 = document.querySelector(".input2");
let result = document.querySelector(".input3");



function checkInputs() {
    if (input1.value === "" && input2.value === "") {
        alert("Please fill in both fields.");
    } else if (input1.value === "") {
        alert("Please fill in the first field.");
    } else if (input2.value === "") {
        alert("Please fill in the second field.");
    }
}


function Sum() {
    checkInputs()
    result.value = Number(input1.value) + Number(input2.value);
}

function Min() {
    checkInputs()
    result.value = Number(input1.value) - Number(input2.value);
}
function Mult() {
    checkInputs()
    result.value = Number(input1.value) * Number(input2.value);
}
function Dvd() {
       if (Number(input2.value) === 0) { 
        alert("You can't divide by zero.");
        return;
    }
    checkInputs()
    result.value = Number(input1.value) / Number(input2.value);
}

function Sum2() {

}

sum.addEventListener("click", Sum);
minus.addEventListener("click", Min);
mult.addEventListener("click", Mult);
dvd.addEventListener("click", Dvd);