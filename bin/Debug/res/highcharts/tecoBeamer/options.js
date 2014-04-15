function init() {
	return {
		chart: {
			type: 'spline'
		},
		title: {
			text: 'Energy'
		},
		xAxis: {
			type: 'datetime',
			text: 'Zeit'
		},
		yAxis: {
			title: {
				text: 'Einheiten'
			},
			labels: {
				formatter: function() {
					return this.value + ' Watt';
				}
			}
		},
		plotOptions: {
			series: {
				marker: {
					enabled: false,
					states: {
						hover: {
							enabled: true
						}
					}
				}
			}
		},
		series: [{
			color: {
				linearGradient: { x1: 0, x2: 0, y1: 0, y1: 1 },
				stops: [
					[0, '#55AA22'],
					[1, '#DD210E']
				]
			},
			name: 'Serie 1',
			data: []
		}],
		credits: {
			enabled: false
		}
	}
};