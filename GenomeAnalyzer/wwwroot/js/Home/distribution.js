document.getElementById('constSeqLength').addEventListener('change', populateStartPosSelect);

$(document).ready(function() {
    $('input[type="radio"]').click(function() {
        if($(this).attr('id') == 'nucRadio') {
            $('#nucParams').show();
            $('#constLenParams').hide();
            $('#ngramParams').hide();
        }

        else if($(this).attr('id') == 'constLenRadio') {
            $('#nucParams').hide();
            $('#constLenParams').show();
            $('#ngramParams').hide();
        }

        else if($(this).attr('id') == 'ngramRadio') {
            $('#nucParams').hide();
            $('#constLenParams').hide();
            $('#ngramParams').show();
        }
    });
});

function populateStartPosSelect() {
    const select = document.getElementById("startPosSelect");
    select.innerHTML = "";
    for (let i = 1; i <= document.getElementById('constSeqLength').value; i++) {
        const option = document.createElement("option");
        option.value = i;
        option.text = i;
        select.add(option);
    }
}