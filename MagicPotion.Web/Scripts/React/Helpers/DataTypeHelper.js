var DataTypeHelper = (function () {

	function isNumber(obj) { return !isNaN(parseFloat(obj)) }

	return {
		isNumber: isNumber
	}
})();