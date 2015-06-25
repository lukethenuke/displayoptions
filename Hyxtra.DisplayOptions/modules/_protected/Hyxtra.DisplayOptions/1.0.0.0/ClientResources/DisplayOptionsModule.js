define([
    'dojo/_base/declare',
    'epi/dependency',
    'epi/routes',

    'epi/_Module'
], function (
    declare,
    dependency,
    routes,

    _Module) {

    return declare([_Module], {

        initialize: function () {
            // summary:
            //      Initializes the module.
            // tags:
            //      public
            var registry = dependency.resolve('epi.storeregistry'),
                path = routes.getRestPath({
                    moduleArea: 'Hyxtra.DisplayOptions',
                    storeName: 'displayoptions',
                    id: ''
                });

            registry.create('hyxtra.displayoptions', path);
        }
    });
});