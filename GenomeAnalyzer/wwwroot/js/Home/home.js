$('#editGenome').on('click', function (e) {
    e.preventDefault();

    let arr = $('#genomeForm').serialize().split('&');
    let data = {
        Id: parseInt(arr[0].split('=')[1]),
        Name: arr[1].split('=')[1],
        Type: parseInt(arr[2].split('=')[1]),
        RawGenome: arr[3].split('=')[1]
    }

    $.ajax({
        type: 'POST',
        url: 'Home/Edit/',
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (response) {
            console.log(response.description);
            location.reload();
        },
        error: function (response) {
            console.log(response.description);
        }
    });
});

$('#createGenome').on('click', function (e){
    e.preventDefault();
    let arr = $('#createGenomeForm').serialize().split('&');
    let data = {
        Name: arr[0].split('=')[1],
        Type: parseInt(arr[1].split('=')[1]),
        RawGenome: arr[2].split('=')[1]
    }

    $.ajax({
        type: 'POST',
        url: 'Home/Create/',
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (response) {
            location.reload();
        },
        error: function (response) {

        }
    });
});

function callModal (id) {
    openModal({
        url: 'Home/GetGenome/',
        data: id,
        content: $('#modal')
    })
}

$('#cancelButton').on('click', function (e) {
    e.preventDefault();
    document.getElementById('createGenomeDiv').style.display = "none";
});

$('#openingForm').on('click', function (e) {
    e.preventDefault();
    document.getElementById('createGenomeDiv').style.display = "block";
});