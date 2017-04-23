//==========================
// 随机抽取数据
//==========================
(function ($)
{
    $.fn.ShowRandom = function (method)
    {
        var temp_time = 0; // 已滚动次数
        var temp_data = new jQuery.Hashtable(); // 已经抽取的数据 Hashtable
        var $this = $(this);

        var methods = {
            init: function (options)
            {
                if (options)
                {
                    $.extend($.fn.ShowRandom.settings, options);
                }

                if (!$.fn.ShowRandom.settings.dataHashtable && $.fn.ShowRandom.settings.dataHashtable.size() > 0)
                {
                    $.error('参数 dataHashtable 数组不能为空！');
                }

                if ($.fn.ShowRandom.settings.dataCount <= 0)
                {
                    $.error('参数 dataCount 必须大于 0 ！');
                }

                if (!$.fn.ShowRandom.settings.allowRepeatData && $.fn.ShowRandom.settings.dataCount > $.fn.ShowRandom.settings.dataHashtable.size())
                {
                    $.error('由于指定抽取的数据量太少，无法抽取指定数量的不重复的数据！');
                }

                if ($.fn.ShowRandom.settings.onCompleteOnce && !jQuery.isFunction($.fn.ShowRandom.settings.onCompleteOnce))
                {
                    $.error('参数 onCompleteOnce 不是事件！');
                }

                if ($.fn.ShowRandom.settings.onAllComplete && !jQuery.isFunction($.fn.ShowRandom.settings.onAllComplete))
                {
                    $.error('参数 onAllComplete 不是事件！');
                }

                if ($.fn.ShowRandom.settings.autoStart)
                {
                    temp_time = 0;
                    temp_data.clear();
                    $.fn.ShowRandom.privatesettings.isStop = false;

                    _showRandom();
                }

                return this;
            },
            start: function ()
            {
                temp_time = 0;
                temp_data.clear();
                $.fn.ShowRandom.privatesettings.isStop = false;

                _showRandom();

                return this;
            },
            stop: function ()
            {
                $.fn.ShowRandom.privatesettings.isStop = true;

                return this;
            }
        };

        if (methods[method])
        {
            return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        }
        else if (typeof method === 'object' || !method)
        {
            return methods.init.apply(this, arguments);
        }
        else
        {
            $.error('方法 ' + method + ' 在 jQuery.fn.ShowRandom 中不存在！');
        }

        function _showRandom()
        {
            if ($.fn.ShowRandom.privatesettings.isStop) return;

            var r = SysRandom(0, $.fn.ShowRandom.settings.dataHashtable.getKeys().length);
            var k = $.fn.ShowRandom.settings.dataHashtable.getKeys()[r];
            var v = $.fn.ShowRandom.settings.dataHashtable.get(k);

            $this.html(v);

            temp_time++;
            if (temp_time >= $.fn.ShowRandom.settings.time)
            {
                temp_data.add(k, v);
                if (!$.fn.ShowRandom.settings.allowRepeatData)
                    $.fn.ShowRandom.settings.dataHashtable.remove(k);

                if ($.fn.ShowRandom.settings.onCompleteOnce)
                    $.fn.ShowRandom.settings.onCompleteOnce(k, v);

                if (temp_data.size() >= $.fn.ShowRandom.settings.dataCount)
                {
                    if ($.fn.ShowRandom.settings.onAllComplete)
                        $.fn.ShowRandom.settings.onAllComplete(temp_data);
                    return;
                }

                temp_time = 0;
            }

            setTimeout(function ()
            {
                _showRandom();
            }, $.fn.ShowRandom.settings.interval);
        }
    };

    $.fn.ShowRandom.settings = {
        autoStart: true,            // 自动开始随机滚动
        'interval': 200,            // 滚动速度，每隔多少毫秒滚动一次
        'time': 20,                 // 每滚动多少次抽取一个数据
        'dataHashtable': null,      // 将要抽取的数据的集合 Hashtable
        'dataCount': 1,             // 总共需要抽取多少数据
        'allowRepeatData': false,   // 是否允许抽取重复数据
        'onCompleteOnce': function (key, value)// 每抽取完成一个数据时触发,参数为已抽取的数据
        {
        },
        'onAllComplete': function (data)// 全部抽取完成时触发,参数为已经抽取的数据 Hashtable
        {
        }
    };
    $.fn.ShowRandom.privatesettings = {
        'isStop': false             // 是否停止
    };
})(jQuery);
