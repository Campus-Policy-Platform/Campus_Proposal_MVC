function chart() {
    let categories = [];
    let categoryLabels = {};
    for (var i = 0; i < catesdata.length; i++) {
        categories.push(catesdata[i].CategoryId);
        categoryLabels[`${catesdata[i].CategoryId}`] = catesdata[i].CategoryName;
    }

    let categoryCounts = categories.map((category) => {
        let count = modeldata.filter((item) => item.CategoryId === category).length;
        return count;
    });
    

    let categoryNames = categories.map((category) => categoryLabels[category]);

    let ctx = document.getElementById('myChart').getContext('2d');
    new Chart(ctx, {
        type: 'pie',
        data: {
            labels: categoryNames,
            datasets: [{
                backgroundColor: [
                    '#daa547',
                    '#8cadf0',
                    '#dab0b1',
                    '#547d79',
                    '#547d79',
                ],
                borderColor: '#ffffff',
                borderWidth: 3,
                label: "案件量",
                data: categoryCounts,
                fill: false,
            }]
        },
        options: {
            plugins: {
                legend: {
                    labels: {
                        color: 'white',
                        font: {
                            size: 20
                        }
                    }
                },
                tooltip: {
                    titleFont: {
                        size: 20
                    },
                    bodyFont: {
                        size: 16
                    }
                }
            }
        }
    });

    let ctx1 = document.getElementById('myChart1');
    if (ctx1 != null) {
        ctx1 = ctx1.getContext('2d');
        let vcategoryCounts = categories.map((category) => {
            let count = vdata.filter((item) => item.CategoryId === category).length;
            return count;
        });
        new Chart(ctx1, {
            type: 'pie',
            data: {
                labels: categoryNames,
                datasets: [{
                    backgroundColor: [
                        '#daa547',
                        '#8cadf0',
                        '#dab0b1',
                        '#547d79',
                        '#547d79',
                    ],
                    borderColor: '#ffffff',
                    borderWidth: 3,
                    label: "案件量",
                    data: vcategoryCounts,
                    fill: false,
                }]
            },
            options: {
                plugins: {
                    legend: {
                        labels: {
                            color: 'white',
                            font: {
                                size: 20
                            }
                        }
                    },
                    tooltip: {
                        titleFont: {
                            size: 20
                        },
                        bodyFont: {
                            size: 16
                        }
                    }
                }
            }
        });
    }
}

window.addEventListener('DOMContentLoaded', (event) => {
    chart();
});