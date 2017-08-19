/* RuleRunner */

function getDescendantProp(obj, desc) {
	var arr = desc.split(".");
	while (arr.length && (obj = obj[arr.shift()]));
	return obj;
}
function _returnMessageFunction(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

var ruleRunner = function ruleRunner(field, name) {
	for (var _len = arguments.length, validations = Array(_len > 2 ? _len - 2 : 0), _key = 2; _key < _len; _key++) {
		validations[_key - 2] = arguments[_key];
	}

	function execute(state) {
		var _iteratorNormalCompletion = true;
		var _didIteratorError = false;
		var _iteratorError = undefined;
		debugger;
		try {
			for (var _iterator = validations[Symbol.iterator](), _step; !(_iteratorNormalCompletion = (_step = _iterator.next()).done); _iteratorNormalCompletion = true) {
				var v = _step.value;
				var prop = getDescendantProp(state, field);
				debugger;
				var errorMessageFunc = v(prop, state);
				if (errorMessageFunc) {
					return _returnMessageFunction({}, field, errorMessageFunc(name));
				}
			}
		} catch (err) {
			_didIteratorError = true;
			_iteratorError = err;
		} finally {
			try {
				if (!_iteratorNormalCompletion && _iterator.return) {
					_iterator.return();
				}
			} finally {
				if (_didIteratorError) {
					throw _iteratorError;
				}
			}
		}

		return null;
	};

	return {
		dataField: field,
		displayName: name,
		execute: execute
	};;
};

var run = function run(state, runners) {
	return runners.reduce(function (memo, runner) {
		return Object.assign(memo, runner.execute(state));
	}, {});
};

/* RuleRunner */

/* Error Messages */

var isRequiredError = function isRequired(fieldName) {
	return fieldName + " is required";
};

var mustMatchError = function mustMatch(otherFieldName) {
	return function (fieldName) {
		return fieldName + " must match " + otherFieldName;
	};
};

var minLengthError = function minLength(length) {
	return function (fieldName) {
		return fieldName + " must be at least " + length + " characters";
	};
};

/* End of Error Messages */

/* Rules */

var requiredRule = function required(obj) {
	if (isNumber(obj)){
		return null;
	}
	if (obj && obj.length > 0) {
		return null;
	} else {
		return isRequiredError;
	}
};

var isNumber = function(obj) { return !isNaN(parseFloat(obj)) }

var mustMatchRule = function mustMatch(field, fieldName) {
	return function (text, state) {
		return getDescendantProp(state, field) === text ? null : mustMatchError(fieldName);
	};
};

var minLengthRule = function minLength(length) {
	return function (text) {
		return text.length >= length ? null : minLengthError(length);
	};
};

/* End of Rules */