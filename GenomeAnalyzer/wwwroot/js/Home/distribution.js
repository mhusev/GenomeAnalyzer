document.getElementById('constSeqLength').addEventListener('change', populateStartPosSelect);

$('#distributeGenome').on('click', function (e) {
    const id = parseInt($('#genomeId').val());
    const radio = document.querySelector('input[type="radio"]:checked').value;
    let data;
    
    if(radio == 'nuc') {
        data = {
            Id: id,
            Nucleotide: $('#nucleotideSelect').val()
        }
    }
    else if(radio == 'constLen') {
        data = {
            Id: id,
            SequenceLength: parseInt($('#constSeqLength').val()),
            StartPosition: parseInt($('#startPosSelect').val())
        }
    }
    else if(radio == 'ngram') {
        data = {
            Id: id,
            SequenceLength: parseInt($('#ngramSeqLength').val())
        }
    }
    
    let qq = JSON.stringify(data);
    
    console.log(qq);
    
    $.ajax({
        type: 'PUT',
        url: '/Home/GetDistributionData/',
        contentType: 'application/json',
        dataType: 'json',
        data: qq,
        success: function (response) {
            console.log(response.description);
            console.log(response.data);
            console.log();
            //location.reload();
        },
        error: function (response) {
            console.log(response.description);
        }
    });
});

$('#cancelButton').on('click', function (e) {
    window.location.assign('/');
});

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