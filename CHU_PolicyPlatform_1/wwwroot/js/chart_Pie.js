function chart() {
    let categories = ["C001", "C002", "C003", "C004"];
    let categoryLabels = {
        "C001": "校园",
        "C002": "教室",
        "C003": "宿舍",
        "C004": "餐廳"
    };

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
}

window.addEventListener('DOMContentLoaded', (event) => {
    chart();
});