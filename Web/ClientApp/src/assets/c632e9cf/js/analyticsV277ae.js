var AnalyticsV2 = AnalyticsV2 || (function () {

    return {
        find: function (id, data) {
            for (let index in data) {
                let item = data[index]
                if (item.id == id) {
                    return item;
                }
            }
        },
        list: function (data) {
            window.dataLayer = window.dataLayer || [];
            dataLayer.push({
                event: 'ProductImpressions',
                ecommerce: {
                    impressions: data
                }
            });
        },
        view: function (data) {
            window.dataLayer = window.dataLayer || [];
            dataLayer.push({
                event: 'productDetail',
                ecommerce: {
                    detail: {
                        actionField: {list: 'Medicines'},
                        products: [data]
                    }
                }
            });
        },
        click: function (data) {
            window.dataLayer = window.dataLayer || [];
            dataLayer.push({
                event: 'productClick',
                ecommerce: {
                    click: {
                        actionField: {list: 'Medicines'},
                        products: [data]
                    }
                }
            });
        },
        addToCart: function (data) {
            if (typeof data.quantity == 'undefined') {
                data.quantity = 1;
            }
            window.dataLayer = window.dataLayer || [];
            dataLayer.push({
                event: 'addToCart',
                ecommerce: {
                    add: {
                        actionField: {list: 'Medicines'},
                        products: [data]
                    }
                }
            });
        },
        removeFromCart: function (data) {
            window.dataLayer = window.dataLayer || [];
            dataLayer.push({
                event: 'removeFromCart',
                ecommerce: {
                    remove: {
                        actionField: {list: 'Medicines'},
                        products: [data]
                    }
                }
            });
        },
        purchase: function (data) {
            window.dataLayer = window.dataLayer || [];
            dataLayer.push({
                event: 'transactionPush',
                ecommerce: {
                    currencyCode: 'UAH',
                    purchase: data
                }
            });

        }
    };
}());