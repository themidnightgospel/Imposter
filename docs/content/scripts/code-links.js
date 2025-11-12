;(function(){
  function enhanceCodeLinks(root){
    try{
      const containers = root.querySelectorAll('.highlight, .md-typeset__highlight');
      containers.forEach(highlight => {
        // Attribute may be on the wrapper or on the code element
        const code = highlight.querySelector('pre > code');
        const url = highlight.getAttribute('data-gh-link') || (code && code.getAttribute('data-gh-link'));
        if(!url) return;

        // Prevent duplicates on re-render
        if (highlight.querySelector('.md-code-gh-link')) return;

        const a = document.createElement('a');
        a.href = url;
        a.target = '_blank';
        a.rel = 'noopener';
        a.className = 'md-code-gh-link';
        a.textContent = 'View on GitHub';

        // Ensure container can position children
        const style = getComputedStyle(highlight);
        if (style.position === 'static') {
          highlight.style.position = 'relative';
        }

        highlight.appendChild(a);
      });
    } catch(e){ /* no-op */ }
  }

  // Initial load
  document.addEventListener('DOMContentLoaded', function(){
    enhanceCodeLinks(document);
  });

  // MkDocs Material instant navigation support
  if (window.document$) {
    window.document$.subscribe(function(){
      enhanceCodeLinks(document);
    });
  }
})();
