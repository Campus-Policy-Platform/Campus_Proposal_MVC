var Show_Pas = document.getElementById("exampleCheck1");
var Pas_Word = document.getElementById("Pas_word");
Show_Pas.addEventListener("change", function (e) {
    if (Show_Pas.checked) {
        Pas_Word.type = 'text';
    }
    else {
        Pas_Word.type = 'password';
    }
});