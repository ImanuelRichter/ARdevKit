function init() {
	return {
		chart: {
			type: 'column'
		},
		title: {
			text: 'Titel'
		},
		subtitle: {
			text: 'Untertitel'
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
					return this.value + '%';
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
					[0, '#DD210E'],
					[1, '#55AA22']
				]
			},
			name: 'Serie 1',
			data: [
				[Date.UTC(2014, 2, 24, 10, 30, 45), 55],
				[Date.UTC(2014, 2, 24, 10, 31, 00), 87]]
		}, {
			type: 'spline',
			name: 'Serie 2',
			data: [
				[Date.UTC(2014, 2, 24, 10, 30, 45), 55],
				[Date.UTC(2014, 2, 24, 10, 31, 00), 87]]
		}],
		credits: {
			enabled: false
		}
	}
};