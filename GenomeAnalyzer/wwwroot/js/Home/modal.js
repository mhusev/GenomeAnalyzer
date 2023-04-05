function openModal (parameters) {
    const id = parameters.data;
    const url = parameters.url;
    const content = parameters.content;
    
    if (id === undefined || url === undefined) {
        alert('Something went wrong.');
        return;
    }
    
    $.ajax({
        type: 'GET',
        url: url,
        data: { "id": id },
        success: function (response) {
            content.find(".modal-body").html(response);
            content.modal('show');
        },
        failure: function () {
            content.modal('hide');
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
};