/**
 * @fileoverview Rule to control usage of strict mode directives.
 * @author Brandon Mills
 */

"use strict";

//------------------------------------------------------------------------------
// Requirements
//------------------------------------------------------------------------------

const astUtils = require("../ast-utils");

//------------------------------------------------------------------------------
// Helpers
//------------------------------------------------------------------------------

const messages = {
    function: "Use the function form of 'use strict'.",
    global: "Use the global form of 'use strict'.",
    multiple: "Multiple 'use strict' directives.",
    never: "Strict mode is not permitted.",
    unnecessary: "Unnecessary 'use strict' directive.",
    module: "'use strict' is unnecessary inside of modules.",
    implied: "'use strict' is unnecessary when implied strict mode is enabled.",
    unnecessaryInClasses: "'use strict' is unnecessary inside of classes.",
    nonSimpleParamet