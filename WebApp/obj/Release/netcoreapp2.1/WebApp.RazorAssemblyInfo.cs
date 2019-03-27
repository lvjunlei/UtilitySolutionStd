'use strict';

exports.__esModule = true;
exports.default = warnOnce;
var printed = {};

function warnOnce(message) {
    if (printed[message]) return;
    printed[message] = true;

    if (typeof console !== 'undefined' && console.warn) console.warn(message);
}
module.exports = exports['default'];
//# sourceMappingURL=data:application/json;charset=utf8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndhcm4tb25jZS5lczYiXSwibmFtZXMiOlsid2Fybk9uY2UiLCJwcmludGVkIiwibWVzc2FnZSIsImNvbnNvbGUiLCJ3YXJuIl0sIm1hcHBpbmdzIjoiOzs7a0JBRXdCQSxRO0FBRnhCLElBQUlDLFVBQVUsRUFBZDs7QUFFZSxTQUFTRCxRQUFULENBQWtCRSxPQUFsQixFQUEyQjtBQUN0QyxRQUFLRCxRQUFRQyxPQUFSLENBQUwsRUFBd0I7QUFDeEJELFlBQVFDLE9BQVIsSUFBbUIsSUFBbkI7O0FBRUEsUUFBSyxPQUFPQyxPQUFQLEtBQW1CLFdBQW5CLElBQWtDQSxRQUFRQyxJQUEvQyxFQUFzREQsUUFBUUMsSUFBUixDQUFhRixPQUFiO0FBQ3pEIiwiZmlsZSI6Indhcm4tb25jZS5qcyIsInNvdXJjZXNDb250ZW50IjpbImxldCBwcmludGVkID0geyB9O1xuXG5leHBvcnQgZGVmYXVsdCBmdW5jdGlvbiB3YXJ