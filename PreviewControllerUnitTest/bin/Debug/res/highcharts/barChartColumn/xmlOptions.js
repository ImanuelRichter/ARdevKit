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
			text: 'Kategorien',
			categories: []
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
		series: [],
		credits: {
			enabled: false
		}
	}
};