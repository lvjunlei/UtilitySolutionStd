#ifndef SASS_POSITION_H
#define SASS_POSITION_H

#include <string>
#include <cstring>
// #include <iostream>

namespace Sass {


  class Offset {

    public: // c-tor
      Offset(const char chr);
      Offset(const char* string);
      Offset(const std::string& text);
      Offset(const size_t line, const size_t column);

      // return new position, incremented by the given string
      Offset add(const char* begin, const char* end);
      Offset inc(const char* begin, const char* end) const;

      // init/create instance from const char substring
      static Offset init(const char* beg, const char* end);

    public: // overload operators for position
      void operator+= (const Offset &pos);
      bool operator== (const Offset &pos) const;
      bool operator!= (const Offset &pos) const;
      Offset operator+ (const Offset &off) const;
      Offset operator- (const Offset &off) const;

    public: // overload output stream operator
      // friend std::ostream& operator<<(std::ostream& strm, const Offset&