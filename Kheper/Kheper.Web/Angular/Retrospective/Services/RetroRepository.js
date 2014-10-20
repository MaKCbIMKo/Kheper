'use strict';

angular.module("kheper.services").factory("retroRepository", function () {
    var retroItems = [];
    return {
        get: function() {
            return retroItems;
        },
        add: function(item) {
            retroItems.push(item);
        },
        remove: function(itemId) {
            
        },
        update: function(item) {
            
        }
    };
});
