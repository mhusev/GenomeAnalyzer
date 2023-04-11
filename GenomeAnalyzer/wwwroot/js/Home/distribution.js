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
            console.log(response.statusCode)
            if (response.statusCode == 200) {
                $('#distributionForm').show();
                fillDistributionForm(response.data);
            }
            else {
                $('#distributionForm').hide();
            }
        },
        error: function (response) {
            console.log(response.description);
        }
    });
});

function fillDistributionForm(data) {
    document.getElementById("entropy").value = data.entropy;
    document.getElementById("firstCentralMoment").value = data.firstCentralMoment;
    document.getElementById("secondCentralMoment").value = data.secondCentralMoment;
    document.getElementById("dispersionCoefficient").value = data.dispersionCoefficient;
    document.getElementById("adenineAmount").value = data.adenineAmount;
    document.getElementById("guanineAmount").value = data.guanineAmount;
    document.getElementById("cytosineAmount").value = data.cytosineAmount;
    document.getElementById("thymineAmount").value = data.thymineAmount;
    document.getElementById("nucleotidesAmount").value = data.nucleotidesAmount;
    document.getElementById("sequencesAmount").value = data.sequencesAmount;
    
    let rankFrequencyTable = buildTable(data.sequencesFrequency, "table table-sm table-striped", ["Sequence", "Frequency"]);
    document.getElementById("rankFrequencyData").appendChild(rankFrequencyTable);

    let statisticalSpectrumTable = buildTable(data.statisticalSpectrum, "table table-sm table-striped", ["Frequency", "Frequency's frequency"]);
    document.getElementById("statisticalSpectrumData").appendChild(statisticalSpectrumTable);
    
    let rankFrequencyChart = new Chart(document.getElementById('rankFrequencyChart').getContext('2d'), {
        type: 'scatter',
        data: {
            labels: Object.keys(data.rankFrequency),
            datasets: [{
                label: 'Rank-frequency distribution',
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1,
                data: Object.values(data.rankFrequency)
            }]
        },
        options: {
            scales: {
                y: {
                    display: true,
                    type: 'logarithmic',
                    suggestedMin: 0.7
                },
                x: {
                    display: true,
                    type: 'logarithmic',
                    suggestedMin: 0.7
                }
            },
            maintainAspectRatio: false,
            responsive: true
        }
    });
    
    let statisticalSpectrumChart = new Chart(document.getElementById('statisticalSpectrumChart').getContext('2d'), {
        type: 'scatter',
        data: {
            labels: Object.keys(data.statisticalSpectrum),
            datasets: [{
                label: 'Statistical Spectrum',
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1,
                data: Object.values(data.statisticalSpectrum)
            }]
        },
        options: {
            scales: {
                y: {
                    display: true,
                    type: 'logarithmic',
                    suggestedMin: 0.7
                },
                x: {
                    display: true,
                    type: 'logarithmic',
                    suggestedMin: 0.7
                }
            },
            maintainAspectRatio: false,
            responsive: true
        }
    });

    $('#distributeGenome').on('click', function (e) {
        rankFrequencyChart.destroy();
        statisticalSpectrumChart.destroy();
        document.getElementById("rankFrequencyData").innerHTML = "";
        document.getElementById("statisticalSpectrumData").innerHTML = "";
    });
};

function buildTable(data, tableClass, headers) {
    let table = document.createElement("table");
    table.className = tableClass;
    let thead = document.createElement("thead");
    let headerRow = document.createElement("tr");
    let headerCell1 = document.createElement("th");
    headerCell1.innerHTML = headers[0];
    let headerCell2 = document.createElement("th");
    headerCell2.innerHTML = headers[1];
    headerRow.appendChild(headerCell1);
    headerRow.appendChild(headerCell2);
    thead.appendChild(headerRow);
    table.appendChild(thead);
    let tbody = document.createElement("tbody");
    for (const [key, value] of Object.entries(data)) {
        const row = document.createElement("tr");
        const cell1 = document.createElement("td");
        cell1.innerHTML = key;
        const cell2 = document.createElement("td");
        cell2.innerHTML = value;
        row.appendChild(cell1);
        row.appendChild(cell2);
        tbody.appendChild(row);
    }
    table.appendChild(tbody);
    
    return table;
};

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