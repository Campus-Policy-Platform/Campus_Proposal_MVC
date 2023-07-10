//置頂
let gotop = document.createElement("a");
gotop.href = "#";
gotop.className = "btn btn-secondary jump jub1";
gotop.innerText = "Top";

//提出提案區
let mine = document.createElement("a");
mine.href = "#Mine";
mine.className = "btn btn-secondary jump jub2";
mine.innerText = "提議";
//提出_分區
let beakmine = document.createElement("div");
beakmine.className = "btn-dark mineb";
let bmine1 = document.createElement("a");
bmine1.href = "#my-underway";
bmine1.className = "btn";
bmine1.innerText = "進行中";
beakmine.append(bmine1);
let bmine2 = document.createElement("a");
bmine2.href = "#my-pass";
bmine2.className = "btn";
bmine2.innerText = "已結案";
beakmine.append(bmine2);

//參與案件區
let voted = document.createElement("a");
voted.href = "#Voted";
voted.className = "btn btn-secondary jump jub3";
voted.innerText = "參與";
//參與_分區
let beakvoted = document.createElement("div");
beakvoted.className = "btn-dark votedb";
let bvoted1 = document.createElement("a");
bvoted1.href = "#voted-underway";
bvoted1.className = "btn";
bvoted1.innerText = "進行中";
beakvoted.append(bvoted1);
let bvoted2 = document.createElement("a");
bvoted2.href = "#voted-pass";
bvoted2.className = "btn";
bvoted2.innerText = "已結案";
beakvoted.append(bvoted2);

window.onload = function () {
    document.body.append(gotop);
    document.body.append(mine);
    document.body.append(voted);
    document.body.append(beakmine);
    document.body.append(beakvoted);
}