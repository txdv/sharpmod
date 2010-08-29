#include <malloc.h>
#include "memory.h"


typedef struct linked_list_node {
  void *ptr;
  int size;
  const void *caller;
  // linked list
  struct linked_list_node *next;
} linked_list;
 
linked_list *linked_list_add(linked_list **p, void *ptr, int size, const void *caller)
{
  if (p == NULL)           /*checks to see if the pointer points somewhere in space*/
    return NULL;
 
  linked_list *n = (linked_list*)malloc(sizeof(linked_list));   /* creates a new node of the correct data size */
  if (n == NULL)
    return NULL;
 
  n->next = *p; /* the previous element (*p) now becomes the "next" element */
  *p = n;       /* add new empty element to the front (head) of the list */
  n->ptr = ptr;
  n->size = size;
  n->caller = caller;
 
  return *p;
}
 
void linked_list_remove(linked_list **p, void *ptr) /* remove head */
{
  if (p != NULL && *p != NULL)
  {
    linked_list *n = *p;
    *p = (*p)->next;
    free(n);
  }
}
 
linked_list **linked_list_search(linked_list **n, void *ptr)
{
  if (n == NULL)
    return NULL;
 
  while (*n != NULL)
  {
    if ((*n)->ptr == ptr)
    {
      return n;
    }
    n = &(*n)->next;
  }
  return NULL;
}

int linked_list_count(linked_list *n)
{
  int i = 0;
  while (n != NULL)
  {
    i++;
    n = n->next;
  }
  return i;
}

void linked_list_print(linked_list *n)
{
  if (n == NULL)
  {
    printf("list is empty\n");
  }
  while (n != NULL)
  {
    printf("list %p %d %d %d\n", n, n->ptr, n->size, n->caller);
    n = n->next;
  }
}


linked_list *ll = NULL;
linked_list **llp = &ll;

static void (*old_free_hook)(void *ptr, const void *caller);
static void *(*old_malloc_hook)(size_t, const void *);

static void *my_malloc_hook (size_t size, const void *caller) {
  void *result;
  __malloc_hook = old_malloc_hook;

  result = malloc(size);
  //list_add(result, size, caller);
  linked_list_add(&ll, result, size, caller);

  old_malloc_hook = __malloc_hook;
  __malloc_hook = my_malloc_hook;
  return result;
}

static void my_free_hook(void *ptr, const void *caller)
{
  __free_hook = old_free_hook;

  free(ptr);
  
  linked_list_remove(&ll, linked_list_search(&ll, ptr));

  old_free_hook = __free_hook;
  __free_hook = my_free_hook;
}


void memory_init()
{
  old_malloc_hook = __malloc_hook;
  __malloc_hook = my_malloc_hook;

  old_free_hook = __free_hook;
  __free_hook = my_free_hook;
}

void memory_uninit()
{
  __malloc_hook = old_malloc_hook;
  __free_hook = old_free_hook;
}

void *memory_list()
{
  int i = (int)llp;
  return (void*)&llp;
}
