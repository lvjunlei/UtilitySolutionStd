                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                01wb�  ���   �8���+X]G+*<eh;cG�M	�'l��	i �	AhS��7�U��?agI�4z|�v�����a�V�{���-��_G�}����j��WQ��u�b�a;P�y�~2���10���4 \*��N0he[D`�<�/�(�K��5k�&h��|�֥.�I��o����VY��$5�G�}���ٛVc�]G�m��R򳂭(���*�Hz(y��#��r�Y �2�Q'Ӌ�����	ء�5p�Qa,��u�B#���A��7���#���E�J�>��.���C�w"~�w}�U���X�"���]���������k��}p �ҷ>�o���#���Vv���//�������1A�tz��["�)R��wSJ�)�܉������+MՍ&���}�볢�zy�ɟx   JUNKN   ]1� [���< i���� �7�0<�z�1{���b��(2V��~9�5�nvST5�E^���ف�>��g�r�.#G�01wb�  ���   �X9�b\C*=#
L�k��A�r�+I��0 R���Q! ����IqK,��6�� �ѕcYV��1��q�=��?�2����Gr�-����g�����i��Ņ~^i4��eϱ4
1x(��D���n�*\�:��#Th�1X�B
$=�cYV�p��w��#�I��A��n�_��;���o�ſ������^i��
���i���<p�Q��yS� ��
)51Ҷ@�q��L
%W�*#C�;�qDSy��%��1�1s�P�3���w���H�D_��ϟ|�/��'���<�|l~�0Տ�D��D�t�,8o�� q^pH����_�J2f��k�6����`PQEZ?F#/���c�[9L��^�; ʑ� z�T�z߸�s��%-���d��D:� `p      JUNKN   ]1� [���< i���� �7�0<�z�1{���b��(2V��~9�5�nvST5�E^���ف�>��g�r�.#G�00dc0
    �P )��@~��P<<R���0xA� ;��� ���A�`~�V��Q���ҁ��@
���@��$�xA� ����q����E*�%�@� <�`�Ѓ�R�< �P� ��7�ސ��(���d0xA� '��$ ���X��U@tK����G��2� �	>_�C�%Q ���^U@��L���-P �������:�����N�ix< ���@>��I�����p����)��*U��@>^M���a@w�O�ǔA�?�V����@_�� �~T߉�4d�<���xD�'ʁ� J���wb�����ȓ��f�r��*����HV�����<���<������ !T�����i�����<��� E>)<��(� �<�4�f�G��?���@B%�"�@��uQd �Є%Z���|>V<�����	0x��C�����a �I����q*��FO�_G��v����N��-���d��8��ݣD��Vx�*Tz�����{���Ƙ�@�dV� �Alu���Z�{_�	B�x��I�/d���
��yRl��w�g�6/&4����+�+�M��^�X#�}k\����;� �oFJ��|Fc�����Dh�S��N��C"��rF��H>��
[D�6�0R#0v�ۮb��`��ψ����\��؀j��+���S���2Z�@*�!�0�08�P��_�n�a��d��p�7Sh\��+8Pw��8�����5╞s��X�{(7�r�ix�	@~o-�n�h��T'���]����ꌑ
-	_EΑ�m�(K���l^:�β����������;�������m:�h^؇��黹�/+<]�,*�9��#��Qp�RG/���x���Gk4??��e(v�a�`�!Ւ@ ��o"S�ozJ=鈗k�狳��+��׵ȵ9�#AkU���H�'use strict';

exports.__esModule = true;

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

/**
 * Represents a plugin’s warning. It can be created using {@link Node#warn}.
 *
 * @example
 * if ( decl.important ) {
 *     decl.warn(result, 'Avoid !important', { word: '!important' });
 * }
 */
var Warning = function () {

  /**
   * @param {string} text        - warning message
   * @param {Object} [opts]      - warning options
   * @param {Node}   opts.node   - CSS node that caused the warning
   * @param {string} opts.word   - word in CSS source that caused the warning
   * @param {number} opts.index  - index in CSS node string that caused
   *                               the warning
   * @param {string} opts.plugin - name of the plugin that created
   *                               this warning. {@link Result#warn} fills
   *                               this property automatically.
   */
  function Warning(text) {
    var opts = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : {};

    _classCallCheck(this, Warning);

    /**
     * @member {string} - Type to filter warnings from
     *                    {@link Result#messages}. Always equal
     *                    to `"warning"`.
     *
     * @example
     * const nonWarning = result.messages.filter(i => i.type !== 'warning')
     */
    this.type = 'warning';
    /**
     * @member {string} - The warning message.
     *
     * @example
     * warning.text //=> 'Try to avoid !important'
     */
    this.text = text;

    if (opts.node && opts.node.source) {
      var pos = opts.node.positionBy(opts);
      /**
       * @member {number} - Line in the input file
       *                    with this warning’s source
       *
       * @example
       * warning.line //=> 5
       */
      this.line = pos.line;
      /**
       * @member {number} - Column in the input file
       *                    with this warning’s source.
       *
       * @example
       * warning.column //=> 6
       */
      this.column = pos.column;
    }

    for (var opt in opts) {
      this[opt] = opts[opt];
    }
  }

  /**
   * Returns a warning position and message.
   *
   * @example
   * warning.toString() //=> 'postcss-lint:a.css:10:14: Avoid !important'
   *
   * @return {string} warning position and message
   */


  Warning.prototype.toString = function toString() {
    if (this.node) {
      return this.node.error(this.text, {
        plugin: this.plugin,
        index: this.index,
        word: this.word
      }).message;
    } else if (this.plugin) {
      return this.plugin + ': ' + this.text;
    } else {
      return this.text;
    }
  };

  /**
   * @memberof Warning#
   * @member {string} plugin - The name of the plugin that created
   *    