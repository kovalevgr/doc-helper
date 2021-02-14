/**
 * Created by noffily on 08.09.16.
 */
function NewSearch() {
    var _defaultOptions = {
        urlSearch: '/search/index/live',
        urlSearchLocation: '/search/index/liveLocations',
        startSearch: '/search/index/getStartSearch',
        container: '#searchAll',
        entity: 'searchAnalysis',
        searchInput: '.all-search__input',
        searchLocationInput: '.locations-all-input',
        resultListLocations: '.loc-search-result',
        resultList: 'ul.livesearch',
        resultBlock: '.search-result',
        notFoundBlock: '.index-no-result',
        messageDanger: '.result-message__danger',
        searchContainer: '.finder__field-icon-doctors',
        locationContainer: '.finder__field-icon-location',
        clearButton: '.js-remove-value',
        type: 'analysis',
        placeholder: {
            ru: 'Введите специальность или фамилию врача',
            uk: 'Введіть спеціальність або прізвище лікаря',
            ua: 'Введіть спеціальність або прізвище лікаря'
        },
        button: {ru: 'Найти врача', uk: 'Знайти лікаря', ua: 'Знайти лікаря'},
        buttonOrder: {"": 'Забронировать', ru: 'Забронировать', uk: 'Забронювати', ua: 'Забронювати'},
        labels: {"": {from: 'от'}, ru: {from: 'от'}, uk: {from: 'від'}, ua: {from: 'від'}},
        icon: 'finder__field-icon-doctors',
        target: '#select-specialization',
        lang: 'ru',
    };

    var _district = '';
    var _metro = '';
    var _search = '';
    var _defaultLink = '';
    var _options = {};

    this.init = function (options, location, district, metro, search, defaultLink) {

        this._district = _district = district ? district : 'all';
        this._metro = _metro = metro ? metro : 'all';
        this._search = _search = search;
        this._defaultLink = _defaultLink = defaultLink;
        this._location = location = location ? location : false;
        this._lang = options.lang;

        var NewSearch = this,
            _input = $(_options.searchInput);
        _district = district ? district : 'all';
        _metro = metro ? metro : 'all';
        _search = search;
        _defaultLink = defaultLink;

        location = location ? location : false;

        _options = _merge_options(_defaultOptions, options);
        NewSearch.autocomplete();
        if (location) {
            NewSearch.autocompleteLocations();
        }
        $(_selector(_options.searchInput)).val(_search);

        var mainDiv = _input.parents('div:first'),
            classesToRemove = ['finder__field-icon-clinics', 'finder__field-icon-diagnostic', 'finder__field-icon-analysis', 'finder__field-icon-actions'];

        $.each(classesToRemove, function (k, v) {
            mainDiv.removeClass(v);
        });

        mainDiv.addClass(_options.icon);

        if (_options.target) {
            mainDiv.attr('data-target', _options.target);
            _input.siblings('div.finder__right-block').attr('data-target', _options.target);
        } else {
            mainDiv.attr('data-target', '');
            _input.siblings('div.finder__right-block').attr('data-target', '');
        }

        mainDiv.siblings('div.finder__submit').find('button').text(_options.button[_options.lang]);
        _input.attr('placeholder', _options.placeholder[_options.lang]);

        return this;
    };

    this.reInit = function (options) {
        options.lang = this._lang;
        this.init(options, this._location, this._district, this._metro, this._search, this._defaultLink)
    };

    this.autocomplete = function () {
        var NewSearch = this;

        $(_selector(_options.searchInput)).on('click', function () {
            if ($(_selector(_options.searchInput)).val() != '') {
                if ($(_selector('.livesearch.top-list')).is(':empty')) {
                    _showNotFound();
                } else {
                    _showSearchResult();
                }
            } else {
                _source(NewSearch, true);
            }
        });

        return $(_selector(_options.searchInput)).autocomplete({
            appendTo: "",
            minLength: 0,
            close: function () {
                _hideSearchResult();
                _hideNotFound();
            },
            source: function (request) {
                if (request.term) {
                    _show(_selector(_options.clearButton));
                    ga('send', 'pageview', '/search.php?query=' + request.term);
                    _source(NewSearch, false, request.term);
                } else {
                    _hide(_selector(_options.clearButton));
                    _source(NewSearch, true);
                }
            }
        });

    };

    this.autocompleteLocations = function () {
        var NewSearch = this;

        $(_selector_location(_options.searchLocationInput)).on('click', function () {

            if ($(_selector_location(_options.searchLocationInput)).val() != '') {
                if ($(_selector_location('.livesearch.top-list')).is(':empty')) {
                    _showNotFoundLocations();
                } else {
                    _showLocationResult();
                }
            } else {

                var data = {
                    district: _district,
                    metro: _metro,
                    type: 'location',
                    entity: _options.container.replace('#', '')
                };

                if (data.entity == 'searchDoctor') {
                    data['specialty'] = _defaultLink;
                } else if (data.entity == 'searchHospital') {
                    data['direction'] = _defaultLink;
                } else if (data.entity == 'searchDc') {
                    data['service'] = _defaultLink;
                } else if (data.entity == 'searchOffer') {
                    data['direction'] = _defaultLink;
                } else {
                    var analysis = _defaultLink.split('/');
                    data['parent_panel'] = analysis[1];
                    data['panel'] = analysis[2];
                }

                $(document).mouseup(function (e) {
                    var container;
                    container = $('.loc-search-result');
                    if (container.has(e.target).length === 0) {
                        container.hide();
                    }
                });


                $.ajax({
                    url: _options.startSearch,
                    dataType: 'json',
                    method: 'GET',
                    data: {
                        district: _district,
                        metro: _metro,
                        type: 'location',
                        language: _options.lang,
                        entity: _options.entity,
                        defaultLink: NewSearch._defaultLink

                    },
                    success: function (data) {
                        _hideLocationResult();
                        _hideNotFoundLocations();

                        NewSearch.clearResultLocations();
                        if (data.status == 'success') {
                            for (var i in data.data) {
                                if (data.data.hasOwnProperty(i)) {
                                    NewSearch.resultAppendLocations(_renderItemLocation(data.data[i]));
                                }
                            }
                            _showLocationResult();
                        } else {
                            _showNotFoundLocations(data.message);
                        }
                    }
                });
            }
        });

        return $(_selector_location(_options.searchLocationInput)).autocomplete({
            appendTo: "",
            minLength: 3,
            close: function () {
                _hideLocationResult();
                _hideNotFoundLocations();
            },
            source: function (request) {
                if (request.term) {
                    _show(_selector_location(_options.clearButton));
                } else {
                    _hide(_selector_location(_options.clearButton));
                }
                $.ajax({
                    url: _options.urlSearchLocation,
                    dataType: 'json',
                    method: 'GET',
                    data: {
                        term: request.term,
                        defaultlink: _defaultLink,
                        type: _options.type,
                        language: _options.lang
                    },
                    success: function (data) {
                        _hideLocationResult();
                        _hideNotFoundLocations();

                        NewSearch.clearResultLocations();
                        if (data.status == 'success') {
                            for (var i in data.data) {
                                if (data.data.hasOwnProperty(i)) {
                                    NewSearch.resultAppendLocations(_renderItemLocation(data.data[i]));
                                }
                            }
                            _showLocationResult();
                        } else {
                            _showNotFoundLocations(data.message);
                        }
                    }
                });
            }
        });
    };

    this.clearResult = function () {
        var li = $(_selector(_options.resultList)).find('li');
        li.remove();
        $(_selector(_options.resultList)).find('div.more-toggle').remove();
    };

    this.clearResultLocations = function () {
        var li = $(_selector_location(_options.resultListLocations)).find('li');
        li.remove();
    };

    this.resultAppend = function (data) {
        $(_selector(_options.resultList)).find('.simplebar-content').append(data);

        $(_selector(_options.resultList)).find('.simplebar-content').on("click", "li a", function (e) {
            var dataValue, title, value;
            value = $(e.currentTarget).data().value;

            if (value) {
                dataValue = $(e.target).data('value');

                var $mainBlock = $("#searchAll"),
                    $childBlock = $mainBlock.find('.filter-search-block'),
                    $finder = $(".finder");

                if ($childBlock.hasClass('searchDoctor')) {
                    $finder.trigger("specializationSelected", {
                        title: title,
                        value: value,
                        dataValue: dataValue
                    });
                } else if ($childBlock.hasClass('searchHospital')) {
                    $finder.trigger("branchSelected", {
                        title: title,
                        value: value
                    });
                } else if ($childBlock.hasClass('searchDc')) {
                    $finder.trigger("diagnosticSelected", {
                        title: title,
                        value: value
                    });
                } else if ($childBlock.hasClass('searchAnalysis')) {

                    $finder.on("analysisSelected", function (e, arg) {
                        var title, value;
                        title = arg.title, value = arg.value;
                        $(".finder input[type='hidden']:first").val(value);
                        return false;
                    });

                    $finder.trigger("analysisSelected", {
                        title: title,
                        value: value,
                        dataValue: dataValue
                    });
                } else {
                    return true;
                }
                return false;
            }
        });


    };

    this.resultAppendLocations = function (data) {
        $(_selector_location(_options.resultListLocations)).find('.simplebar-content').append(data);

        $(_selector_location(_options.resultListLocations)).find('.simplebar-content').on("click", "li a", function (e) {

            var dataValue, title, value;

            value = $(e.currentTarget).data().value;
            if (!value) {
                value = title;
            }

            dataValue = $(e.target).data('value');

            if (!dataValue) {
                return true;
            }

            var $mainBlock = $("#searchAll"),
                $childBlock = $mainBlock.find('.filter-search-block'),
                $finder = $(".finder");

            $("#selectArea").val(dataValue);

            if ($childBlock.hasClass('searchDoctor')) {
                $finder.trigger("specializationSelected", {
                    title: title,
                    value: $('#selectService').val(),
                    dataValue: dataValue
                });
            } else if ($childBlock.hasClass('searchHospital')) {
                $finder.trigger("branchSelected", {
                    title: title,
                    value: $('#selectService').val()
                });
            } else if ($childBlock.hasClass('searchDc')) {
                $finder.trigger("diagnosticSelected", {
                    title: title,
                    value: $('#selectService').val()
                });
            } else if ($childBlock.hasClass('searchAnalysis')) {
                $finder.on("analysisSelected", function (e, arg) {
                    var title, value;
                    title = arg.title, value = arg.value;
                    $(".finder input[type='hidden']:first").val(value);
                    return false;
                });

                $finder.trigger("analysisSelected", {
                    title: title,
                    value: $('#selectService').val(),
                    dataValue: dataValue
                });
            }
            return false;
        });

    };

    function _renderItemLocation(data) {
        var hostname = location.hostname.replace('lab.', '');
        var alias = '';
        var dataValue;

        if (_options.type == 'analysis') {
            alias = window.location.protocol + '//lab.' + hostname + data.alias;

            if (data.id) {
                dataValue = alias.match(/(metro\/)*[0-9a-z\-]+$/);
                dataValue[0] = dataValue[0].replace('all', '');
            }
        } else {
            alias = window.location.protocol + '//' + hostname + data.alias;
            if (data.id) {
                dataValue = alias.match(/(metro\/)*[0-9a-z\-]+$/);
            }
        }

        var type = '';

        if (data.type == 'point') {
            type = 'is-area';
        } else if (data.type == 'blue') {
            type = 'is-blue';
        } else if (data.type == 'red') {
            type = 'is-red';
        } else if (data.type == 'green') {
            type = 'is-green';
        }

        return '<li class="location-lists__item js-lists-item ' + type + '">' +
            '<a href="' + alias + '">' +
            data.name +
            '</a></li>';
    }

    function _renderItem(data) {

        var categories = ['specialty', 'hospital_direction', 'dcservices_all', 'panel'];

        var hostname = location.hostname.replace('lab.', '');
        var alias = '';

        var dataValue;

        if (data.type_us == 'laboratory' || data.type_us == 'analysis' || data.type_us == 'panel') {
            alias = window.location.protocol + '//lab.' + hostname + data.alias;
            if (categories.indexOf(data.type_us) !== -1) {
                dataValue = alias.match(/(metro\/)*all\/[0-9a-z\-\/\_]*/);
                dataValue[0] = dataValue[0].replace('all', '');
            }
        } else {
            alias = (window.location.protocol + '//' + hostname + data.alias).replace('lab.', '');
            if (categories.indexOf(data.type_us) !== -1) {
                dataValue = alias.match(/(metro\/)*[0-9a-z\_\-]+$/);
            }
        }

        return '<li class="top-list__item">' +
            '<div class="top-list__item_line">' +
            '<a href="' + alias + '" title="" class="top-list__item-link">' +
            '<div class="top-list__picture"><img src="' + data.icon + '" alt=""></div>' +
            '<div class="top-list__left">' +
            '<div class="top-list__title"><div class="top-list__title_label">' + data.type + '</div>' + data.name + '</div>' +
            '</div>' +
            '<div class="top-list__right">' +
            (data.price ? '<div class="top-list__price f-b">' + _defaultOptions.labels[_options.lang].from + ' <span>' + data.price + '</span> грн</div>' : '') +
            '</div>' +
            '</a>' +
            (data.price ? '<button class="offers_card_point_button" data-medicine_id="' + data.id + '" data-customer_id="0" data-count="1">' + _defaultOptions.buttonOrder[_options.lang] + '</button> ' : '') +
            '</div>' +
            '</li>';
    }

    function _showLocationResult() {
        _show(_selector_location(_options.resultListLocations));
    }

    function _hideLocationResult() {
        _hide(_selector_location(_options.resultListLocations));
    }

    function _showSearchResult() {
        _show(_selector(_options.resultBlock));
    }

    function _hideSearchResult() {
        _hide(_selector(_options.resultBlock));
    }

    function _showNotFound(message) {
        message = message ? message : false;
        if (message) {
            $(_selector(_options.messageDanger)).empty().append(message);
        }
        _show(_selector(_options.notFoundBlock));
    }

    function _showNotFoundLocations(message) {
        message = message ? message : false;
        if (message) {
            $(_selector_location(_options.messageDanger)).empty().append(message);
        }
        _show(_selector_location(_options.notFoundBlock));
    }

    function _hideNotFound() {
        _hide(_selector(_options.notFoundBlock));
    }

    function _hideNotFoundLocations() {
        _hide(_selector_location(_options.notFoundBlock));
    }

    function _show(attribute) {
        $(attribute).css('display', 'block');
    }

    function _hide(attribute) {
        $(attribute).css('display', 'none');
    }

    function _selector(selector) {
        return _options.container + ' ' + _options.searchContainer + ' ' + selector;
    }

    function _selector_location(selector) {
        return _options.container + ' ' + _options.locationContainer + ' ' + selector;
    }

    function _merge_options(obj1, obj2) {
        var obj3 = {};

        for (var i1 in obj1) {
            if (obj1.hasOwnProperty(i1)) {
                obj3[i1] = obj1[i1];
            }
        }

        for (var i2 in obj2) {
            if (obj2.hasOwnProperty(i2)) {
                obj3[i2] = obj2[i2];
            }
        }

        return obj3;
    }


    function _source(NewSearch, start, term) {
        var data = [];
        if (start) {
            data = {
                type: _options.type,
                district: _district,
                metro: _metro,
                language: _options.lang
            };
        } else {
            data = {
                term: term,
                district: _district,
                metro: _metro,
                type: _options.type,
                language: _options.lang
            };
        }

        if (NewSearch._defaultLink != '') {
            data['defaultLink'] = NewSearch._defaultLink;
        }

        $.ajax({
            url: start ? _options.startSearch : _options.urlSearch,
            dataType: 'json',
            method: 'GET',
            data: data,
            success: function (data) {
                _hideSearchResult();
                _hideNotFound();

                NewSearch.clearResult();
                if (data.status == 'success') {
                    for (var i in data.data) {
                        if (data.data.hasOwnProperty(i)) {
                            NewSearch.resultAppend(_renderItem(data.data[i]));
                        }
                    }
                    if (!start) {
                        NewSearch.resultAppend(data.message);
                    }
                    _showSearchResult();
                } else {
                    _showNotFound(data.message);
                }
            }
        });
    }
}

$(document).on('click', '.js-remove-value', function (event) {
    var input;
    input = $(this).siblings('input');
    $(this).hide();
    input.val('');
    if ($('.js-lists').hasClass('is-show')) {
        $('.js-lists').hide();
        $('.js-lists').removeClass('is-show');
    }
    $(this).siblings('.index-no-result.result-message__danger').hide();
    input.click();
    event.stopPropagation();
    return false;
});


