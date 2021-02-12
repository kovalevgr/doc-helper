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

	$("#select-area").on("areaSelected", function(){
        $('.finder__submit button').click();
	});
    $('.finder').on('specializationSelected', function(){
        $(".finder__submit button").click();
    });

	$('.finder__submit button').on('click', function (e) {
		var specialty = $(".finder [data-target='#select-specialization'] input[type='hidden']").val();
		var districts = $(".finder [data-target='#select-area'] input[type='hidden']").val().split(', ').unique_doc();
		var districts_val = $(".finder [data-target='#select-area'] input[type='hidden']").val();
		if (districts_val == '') {
			districts = ['all'];
		}

		var metro_alis = '';
		if ((typeof $(".nav.nav-pills").find(".active a[href='#select-area-metro']").html() != 'undefined') & districts_val != '') {
			metro_alis = '/metro'
		}

		var new_location = '/doctors/' + current_city + metro_alis + '/' + districts.join('+');
		if (specialty != '' && specialty != 'all') {
			new_location += '/' + specialty
		}

		window.location = new_location;

		return false;

	});
});