//------------------------------------
let arr = [1, 2, 2, 3, 3, 3, 4];

function removeAndCount(arr) {
    let count = {};
    let arr2 = [];

    for (let num of arr) {
        if (count[num]) {
            count[num]++;
        } else {
            count[num] = 1;
        }

        if (!arr2.includes(num)) {
            arr2.push(num);
        }
    }
    return { arr2, count };

}

//--------------------------------------
function isPalindrome(str) {
    str = str.toLowerCase().replace(/\s/g, "");
    let reverse = str.split("").reverse().join("");
    return str === reverse;
}


//-----------------------------------------

function countSmaller(arr, num) {
    let count = 0;

    for (let item of arr) {
        if (item < num) {
            count++;

        }
    }

    return count;
}
//--------------------------------------
function abundantOrDeficient(num) {
    let sum = 0;

    for (let i = 1; i <= num / 2; i++) {
        if (num % i === 0) sum += i;
    }

    if (sum > num) {
        return "Abundant";
    }

    if (sum < num) {
        return "Deficient";

    }
    else
    {
        return "Not abudant or deficient";
    }
}
//---------------------------------------

function squareArr(arr) {
    return arr.map(x => x * x);
}

console.log(removeAndCount(arr));
console.log(squareArr([1, 2, 3, 4]));
console.log(isPalindrome("davad"));
console.log(isPalindrome("loler"));
console.log(countSmaller([2, 1, 45, 10, 5, 6], 5));
console.log(abundantOrDeficient(12)); 
console.log(abundantOrDeficient(13));
console.log(abundantOrDeficient(6));








