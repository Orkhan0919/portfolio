
let card = document.createElement("div");
card.style.width = "320px";
card.style.border = "1px solid #ddd";
card.style.borderRadius = "10px";
card.style.overflow = "hidden";
card.style.fontFamily = "Arial, sans-serif";
card.style.boxShadow = "0 6px 18px rgba(0,0,0,0.08)";
card.style.backgroundColor = "#fff";
card.style.margin = "24px";

let img = document.createElement("img");
img.src = "../assets/JS/img1.webp";
img.alt = "Detached house";
img.style.display = "block";
img.style.width = "100%";
img.style.height = "200px";
img.style.objectFit = "cover";

let card2 = document.createElement("div");
card.style.padding = "12px";

let title = document.createElement("div");
title.textContent = "DETACHED HOUSE  5Y OLD";
title.style.fontSize = "14px";
title.style.fontWeight = "700";
title.style.color = "#0f172a";
title.style.marginTop = "30px";


let price = document.createElement("div");
price.textContent = "$750,000";
price.style.marginTop = "8px";
price.style.fontSize = "30px";
price.style.fontWeight = "800";

let pr = document.createElement("div");
pr.textContent = "742 Evergreen Terrace";
pr.style.marginTop = "18px";
pr.style.fontSize = "10px";
pr.style.fontWeight = "800";


let pr2 = document.createElement("div");
pr2.textContent = "3 Bedrooms  ⠀   ⠀ ⠀   ⠀ 2 Bathrooms";
pr2.style.marginTop = "28px";
pr2.style.fontSize = "17px";
pr2.style.fontWeight = "800";

let pr3 = document.createElement("div");
pr3.textContent = "Realtor";
pr3.style.marginTop = "22px";
pr3.style.fontSize = "16px";
pr3.style.fontWeight = "800";


card2.appendChild(title);
card2.appendChild(price);
card2.appendChild(pr);
card2.appendChild(pr2);
card2.appendChild(pr3);


card.appendChild(img);
card.appendChild(card2);

document.body.style.margin = "0";
document.body.style.display = "flex";
document.body.style.justifyContent = "center";
document.body.style.alignItems = "center";
document.body.style.minHeight = "100vh";
document.body.style.background = "#f4f6fb";

document.body.appendChild(card);

let profileImg = document.createElement("img");
profileImg.src = "../assets/JS/img2.webp";
profileImg.alt = "Profile";
profileImg.style.width = "60px";
profileImg.style.height = "60px";
profileImg.style.borderRadius = "50%";
profileImg.style.marginTop = "12px";
profileImg.style.objectFit = "cover";
profileImg.style.display = "inline-block";
profileImg.style.verticalAlign = "middle";



let profileName = document.createElement("span");
profileName.textContent = "Rauf Memmedli";
profileName.style.marginLeft = "12px";
profileName.style.fontSize = "16px";
profileName.style.fontWeight = "700";
profileName.style.color = "#0f172a";
profileName.style.display = "inline-block";
profileName.style.verticalAlign = "middle";

let num = document.createElement("span");
num.textContent = "Number : 055-555-55-55";
num.style.fontSize = "16px";
num.style.marginLeft = "12px";
num.style.fontWeight = "700";
num.style.color = "#0f172a";
num.style.verticalAlign = "middle";
num.style.display = "flex";
num.style.marginTop = "30px";




card2.appendChild(profileImg);
card2.appendChild(profileName);
card2.appendChild(num);



