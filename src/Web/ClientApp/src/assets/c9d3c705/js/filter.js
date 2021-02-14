$(function () {

	Array.prototype.unique_doc = function () {
		var unique = [];
		for (var i = 0; i < this.length; i++) {
			if (unique.indexOf(this[i]) == -1) {
				unique.push(this[i]);
			}
		}
		return unique;
	};

	$("#filter_search").on("change", function(){

		return;

		var filterSearchBlock = $(".filter-search-block");
			filterSearchBlock.removeClass("searchDoctor searchHospital searchDc searchAnalysis searchOffer");

		var $this = $(this),
			params = {};

		switch ($this.val()){
			case 'Doctor':
				params = {type: "doctors", entity: "searchDoctor",formName:'search_doctor_form'};
				filterSearchBlock.addClass("searchDoctor");
				break;
			case 'Hospital':
				params = {type: "kliniki", entity: "searchHospital",icon:'finder__field-icon-clinics',placeholder:{ru:'Введите направление или название клиник',uk:'Введіть напрям або назву клініки'},target:'#select-branch',button:{ru:'Найти клинику',uk:'Знайти клініку'},formName:'search_hospital_form'};
				filterSearchBlock.addClass("searchHospital");
				break;
			case 'Dc':
				params = {type: "diagnostics", entity: "searchDc",icon:'finder__field-icon-diagnostic',placeholder:{ru:'Введите вид диагностики или название центра',uk:'Введіть вид діагностики або назву центру'},target:'#select-area-diagnostics',button:{ru:'Найти',uk:'Знайти'},formName:'search_dc_form'};
				filterSearchBlock.addClass("searchDc");
				break;
			case 'Analysis':
				params = {
					type: "analysis",
					icon: 'finder__field-icon-analysis',
					placeholder: {
						ru: 'Введите название анализа или название центра',
						uk: 'Введіть назву аналізу або назву центру'
					},
					target:'#select-area-analysis',
					button: {ru: 'Найти', uk: 'Знайти'},
				};
				filterSearchBlock.addClass("searchAnalysis");
				break;

			case 'Offer':
				params = {
					type: "specialoffer",
					icon: 'finder__field-icon-actions',
					entity: "searchOffer",
					placeholder: {
						ru: 'Введите название акционного предложения или направления',
						uk: 'Введіть назву акційної пропозицiї або напрямку'
					},
					target:'#select-actions',
					button: {ru: 'Найти', uk: 'Знайти'},
				};
				filterSearchBlock.addClass("searchOffer");
				break;
		}
		newSearcher.reInit(params);
	});

	$('.finder').on('specializationSelected', function(){
		submitFilter('doctor');
	});

	$('.finder').on('branchSelected', function(e,param,param2){
		if(param.services){
			submitFilter('uslugi');
		}else{
			submitFilter('clinic');
		}
	});

	$('.finder').on('diagnosticSelected', function(){
		submitFilter('dc');
	});

	$('.finder').on('analysisSelected', function(){
		submitFilter('analysis');
	});

	$('.finder').on('actionsSelected', function(){
		submitFilter('specialoffer');
	});

	$("#select-area").on("areaSelected", function(){

		if ($('.searchDoctor').is(":visible")) {
			submitFilter('doctor');
		}
		if ($('.searchHospital').is(":visible")) {
			submitFilter('clinic');
		}
		if ($('.searchDc').is(":visible")) {
			submitFilter('dc');
		}
		if ($('.searchAnalysis').is(":visible")) {
			submitFilter('analysis');
		}
		if ($('.select-service-area').length > 0) {
			submitFilter('uslugi');
		}
		if ($('.searchOffer').is(":visible")) {
			submitFilter('specialoffer');
		}

		if ($('.searchService').is(":visible")) {
			submitFilter('uslugi');
		}

	});

	function submitFilter(type) {

		switch (type) {
			case 'doctor':
				var specialty = $("#selectService").val();

				var districts = $("#selectArea").val().split(', ').unique_doc();
				var districts_val = $("#selectArea").val();
				if (districts_val == '' || districts_val == 'undefined') {
					districts = ['all'];
				}

				var new_location = '/doctors/' + current_city + '/' + districts.join('+');

				if (specialty != '' && specialty != 'all') {
					new_location += '/' + specialty
				}

				new_location = new_location.replace('//','/');

				break;
			case 'clinic':
				var direct = $("#selectService").val();
				var districts = $("#selectArea").val().split(', ').unique_doc();
				var districts_val = $("#selectArea").val();
				if (districts_val == '') {
					districts = ['all'];
				}

				var new_location = '/kliniki/' + current_city + '/' + districts.join('+');
				if (direct != '' && direct != 'all') {
					new_location += '/' + direct
				}

				new_location = new_location.replace('//','/');
				break;
			case 'dc':
				var districts = $("#selectArea").val().split(', ').unique_doc();
				var districts_val = $("#selectArea").val();
				if (districts_val == '' || districts_val == 'undefined') {
					districts = ['all'];
				}
				var new_location = '/diagnostics/' + current_city + '/' + districts.join('+');

				var diagnostics_val = $("#selectService").val();
				if (diagnostics_val != '' && diagnostics_val != 'all') {
					new_location += '/' + diagnostics_val
				}
				new_location = new_location.replace('//','/');
				break;
			case 'analysis':
				var direction = $("#selectService").val();
				var districts = $("#selectArea").val().split(', ').unique_doc();
				var districts_val = $("#selectArea").val();

				if (districts_val == '' || districts_val == 'undefined') {
					districts = ['all'];
				}

				var data =  '/' + districts.join('+');
				var url = '';

				if (typeof default_url == 'undefined') {
					url = index_analysis;
				} else {
					url = default_url;
				}

				var new_location = url.replace('/all', data);

				/*if (direction != '' && direction != 'all') {
					new_location += direction
				}*/

				break;
			case 'uslugi':
				var direct = $("#selectService").val(),
					direction = $("#selectService").val(),
					patternDistrict = new RegExp(direction+'([a-z0-9\/\\\-\#\_])*$'),
					url = location.href.toString().match(patternDistrict);

				if (url && url[0]){
					direct = url[0].replace(/\#[a-z0-9\-]+$/,'').replace(/\/page-\d/,'');
				}

				var districts = $("#selectArea").val().split(', ').unique_doc();
				var districts_val = $("#selectArea").val();
				if (districts_val == '' || districts_val == 'undefined') {
					districts = ['all'];
				}

				var new_location = '/uslugi/' + current_city + '/' + districts.join('+');
				if (direct != '' && direct != 'all') {
					new_location += '/' + direct
				}

				break;
			case 'specialoffer':
				var direct = $("#selectService").val();
				var districts = $("#selectArea").val().split(', ').unique_doc();
				var districts_val = $("#selectArea").val();
				if (districts_val == '' || districts_val == 'undefined') {
					districts = ['all'];
				}

				var new_location = '/promotions/' + current_city + '/' + districts.join('+');
				if (direct != '' && direct != 'all') {
					new_location += '/' + direct
				}
				new_location = new_location.replace('//','/');
				break;
		}

		var language = $('#selectLanguage').val();

		if(language !== undefined){
			new_location = '/'+language+new_location;
		}

		//if (type != 'dc' || type != 'analysis') {
			var hostname = window.location.hostname.replace('lab.', '');

			if (new_location.indexOf('/uslugi') !== -1 || new_location.indexOf('/kliniki') !== -1 || new_location.indexOf('/diagnostics') !== -1 || new_location.indexOf('/doctors') !== -1) {

				window.location = window.location.protocol + '//' + hostname + new_location
				/*if (new_location.indexOf('/uslugi') !== -1) {
					window.location = window.location.protocol + '//' + hostname + new_location + '#visit-place';
				} else {
					window.location = window.location.protocol + '//' + hostname + new_location
				}*/
			} else {
				window.location = new_location;
			}
		//}
		return false;
	}
});