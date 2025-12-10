<script>
    $(document).ready(function () {
        $('input[required]').each(function () {

            var $label = $(this).prev('label');
            $label.addClass('required');
        });
        });
</script>