function init() {
	return {
        	chart: {
        	},
        	title: {
            		text: 'Titel'
        	},
        	tooltip: {
    	    		pointFormat: ''
        	},
        	plotOptions: {
            			pie: {
                			allowPointSelect: true,
                			cursor: 'pointer',
                			dataLabels: {
                    			enabled: true,
                    			color: '#000000',
                    			connectorColor: '#000000',
                    			format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                		}
            		}
        	},
        	series: [
			['SerieA',   45.0],
               	 	['SerieB',   55.0]
        	}]
    	};
};