/* Generated By:JavaCC: Do not edit this line. SimpleCharStream.java Version 4.0 */
using System;
using System.IO;


namespace Org.Apache.Hadoop.Record.Compiler.Generated
{
	/// <summary>
	/// An implementation of interface CharStream, where the stream is assumed to
	/// contain only ASCII characters (without unicode processing).
	/// </summary>
	[System.ObsoleteAttribute(@"Replaced by <a href=""http://hadoop.apache.org/avro/"">Avro</a>."
		)]
	public class SimpleCharStream
	{
		public const bool staticFlag = false;

		internal int bufsize;

		internal int available;

		internal int tokenBegin;

		public int bufpos = -1;

		protected internal int[] bufline;

		protected internal int[] bufcolumn;

		protected internal int column = 0;

		protected internal int line = 1;

		protected internal bool prevCharIsCR = false;

		protected internal bool prevCharIsLF = false;

		protected internal StreamReader inputStream;

		protected internal char[] buffer;

		protected internal int maxNextCharInd = 0;

		protected internal int inBuf = 0;

		protected internal int tabSize = 8;

		protected internal virtual void SetTabSize(int i)
		{
			tabSize = i;
		}

		protected internal virtual int GetTabSize(int i)
		{
			return tabSize;
		}

		protected internal virtual void ExpandBuff(bool wrapAround)
		{
			char[] newbuffer = new char[bufsize + 2048];
			int[] newbufline = new int[bufsize + 2048];
			int[] newbufcolumn = new int[bufsize + 2048];
			try
			{
				if (wrapAround)
				{
					System.Array.Copy(buffer, tokenBegin, newbuffer, 0, bufsize - tokenBegin);
					System.Array.Copy(buffer, 0, newbuffer, bufsize - tokenBegin, bufpos);
					buffer = newbuffer;
					System.Array.Copy(bufline, tokenBegin, newbufline, 0, bufsize - tokenBegin);
					System.Array.Copy(bufline, 0, newbufline, bufsize - tokenBegin, bufpos);
					bufline = newbufline;
					System.Array.Copy(bufcolumn, tokenBegin, newbufcolumn, 0, bufsize - tokenBegin);
					System.Array.Copy(bufcolumn, 0, newbufcolumn, bufsize - tokenBegin, bufpos);
					bufcolumn = newbufcolumn;
					maxNextCharInd = (bufpos += (bufsize - tokenBegin));
				}
				else
				{
					System.Array.Copy(buffer, tokenBegin, newbuffer, 0, bufsize - tokenBegin);
					buffer = newbuffer;
					System.Array.Copy(bufline, tokenBegin, newbufline, 0, bufsize - tokenBegin);
					bufline = newbufline;
					System.Array.Copy(bufcolumn, tokenBegin, newbufcolumn, 0, bufsize - tokenBegin);
					bufcolumn = newbufcolumn;
					maxNextCharInd = (bufpos -= tokenBegin);
				}
			}
			catch (Exception t)
			{
				throw new Error(t.Message);
			}
			bufsize += 2048;
			available = bufsize;
			tokenBegin = 0;
		}

		/// <exception cref="System.IO.IOException"/>
		protected internal virtual void FillBuff()
		{
			if (maxNextCharInd == available)
			{
				if (available == bufsize)
				{
					if (tokenBegin > 2048)
					{
						bufpos = maxNextCharInd = 0;
						available = tokenBegin;
					}
					else
					{
						if (tokenBegin < 0)
						{
							bufpos = maxNextCharInd = 0;
						}
						else
						{
							ExpandBuff(false);
						}
					}
				}
				else
				{
					if (available > tokenBegin)
					{
						available = bufsize;
					}
					else
					{
						if ((tokenBegin - available) < 2048)
						{
							ExpandBuff(true);
						}
						else
						{
							available = tokenBegin;
						}
					}
				}
			}
			int i;
			try
			{
				if ((i = inputStream.Read(buffer, maxNextCharInd, available - maxNextCharInd)) ==
					 -1)
				{
					inputStream.Close();
					throw new IOException();
				}
				else
				{
					maxNextCharInd += i;
				}
				return;
			}
			catch (IOException e)
			{
				--bufpos;
				Backup(0);
				if (tokenBegin == -1)
				{
					tokenBegin = bufpos;
				}
				throw;
			}
		}

		/// <exception cref="System.IO.IOException"/>
		public virtual char BeginToken()
		{
			tokenBegin = -1;
			char c = ReadChar();
			tokenBegin = bufpos;
			return c;
		}

		protected internal virtual void UpdateLineColumn(char c)
		{
			column++;
			if (prevCharIsLF)
			{
				prevCharIsLF = false;
				line += (column = 1);
			}
			else
			{
				if (prevCharIsCR)
				{
					prevCharIsCR = false;
					if (c == '\n')
					{
						prevCharIsLF = true;
					}
					else
					{
						line += (column = 1);
					}
				}
			}
			switch (c)
			{
				case '\r':
				{
					prevCharIsCR = true;
					break;
				}

				case '\n':
				{
					prevCharIsLF = true;
					break;
				}

				case '\t':
				{
					column--;
					column += (tabSize - (column % tabSize));
					break;
				}

				default:
				{
					break;
				}
			}
			bufline[bufpos] = line;
			bufcolumn[bufpos] = column;
		}

		/// <exception cref="System.IO.IOException"/>
		public virtual char ReadChar()
		{
			if (inBuf > 0)
			{
				--inBuf;
				if (++bufpos == bufsize)
				{
					bufpos = 0;
				}
				return buffer[bufpos];
			}
			if (++bufpos >= maxNextCharInd)
			{
				FillBuff();
			}
			char c = buffer[bufpos];
			UpdateLineColumn(c);
			return (c);
		}

		public virtual int GetEndColumn()
		{
			return bufcolumn[bufpos];
		}

		public virtual int GetEndLine()
		{
			return bufline[bufpos];
		}

		public virtual int GetBeginColumn()
		{
			return bufcolumn[tokenBegin];
		}

		public virtual int GetBeginLine()
		{
			return bufline[tokenBegin];
		}

		public virtual void Backup(int amount)
		{
			inBuf += amount;
			if ((bufpos -= amount) < 0)
			{
				bufpos += bufsize;
			}
		}

		public SimpleCharStream(StreamReader dstream, int startline, int startcolumn, int
			 buffersize)
		{
			inputStream = dstream;
			line = startline;
			column = startcolumn - 1;
			available = bufsize = buffersize;
			buffer = new char[buffersize];
			bufline = new int[buffersize];
			bufcolumn = new int[buffersize];
		}

		public SimpleCharStream(StreamReader dstream, int startline, int startcolumn)
			: this(dstream, startline, startcolumn, 4096)
		{
		}

		public SimpleCharStream(StreamReader dstream)
			: this(dstream, 1, 1, 4096)
		{
		}

		public virtual void ReInit(StreamReader dstream, int startline, int startcolumn, 
			int buffersize)
		{
			inputStream = dstream;
			line = startline;
			column = startcolumn - 1;
			if (buffer == null || buffersize != buffer.Length)
			{
				available = bufsize = buffersize;
				buffer = new char[buffersize];
				bufline = new int[buffersize];
				bufcolumn = new int[buffersize];
			}
			prevCharIsLF = prevCharIsCR = false;
			tokenBegin = inBuf = maxNextCharInd = 0;
			bufpos = -1;
		}

		public virtual void ReInit(StreamReader dstream, int startline, int startcolumn)
		{
			ReInit(dstream, startline, startcolumn, 4096);
		}

		public virtual void ReInit(StreamReader dstream)
		{
			ReInit(dstream, 1, 1, 4096);
		}

		/// <exception cref="System.IO.UnsupportedEncodingException"/>
		public SimpleCharStream(InputStream dstream, string encoding, int startline, int 
			startcolumn, int buffersize)
			: this(encoding == null ? new InputStreamReader(dstream) : new InputStreamReader(
				dstream, encoding), startline, startcolumn, buffersize)
		{
		}

		public SimpleCharStream(InputStream dstream, int startline, int startcolumn, int 
			buffersize)
			: this(new InputStreamReader(dstream), startline, startcolumn, buffersize)
		{
		}

		/// <exception cref="System.IO.UnsupportedEncodingException"/>
		public SimpleCharStream(InputStream dstream, string encoding, int startline, int 
			startcolumn)
			: this(dstream, encoding, startline, startcolumn, 4096)
		{
		}

		public SimpleCharStream(InputStream dstream, int startline, int startcolumn)
			: this(dstream, startline, startcolumn, 4096)
		{
		}

		/// <exception cref="System.IO.UnsupportedEncodingException"/>
		public SimpleCharStream(InputStream dstream, string encoding)
			: this(dstream, encoding, 1, 1, 4096)
		{
		}

		public SimpleCharStream(InputStream dstream)
			: this(dstream, 1, 1, 4096)
		{
		}

		/// <exception cref="System.IO.UnsupportedEncodingException"/>
		public virtual void ReInit(InputStream dstream, string encoding, int startline, int
			 startcolumn, int buffersize)
		{
			ReInit(encoding == null ? new InputStreamReader(dstream) : new InputStreamReader(
				dstream, encoding), startline, startcolumn, buffersize);
		}

		public virtual void ReInit(InputStream dstream, int startline, int startcolumn, int
			 buffersize)
		{
			ReInit(new InputStreamReader(dstream), startline, startcolumn, buffersize);
		}

		/// <exception cref="System.IO.UnsupportedEncodingException"/>
		public virtual void ReInit(InputStream dstream, string encoding)
		{
			ReInit(dstream, encoding, 1, 1, 4096);
		}

		public virtual void ReInit(InputStream dstream)
		{
			ReInit(dstream, 1, 1, 4096);
		}

		/// <exception cref="System.IO.UnsupportedEncodingException"/>
		public virtual void ReInit(InputStream dstream, string encoding, int startline, int
			 startcolumn)
		{
			ReInit(dstream, encoding, startline, startcolumn, 4096);
		}

		public virtual void ReInit(InputStream dstream, int startline, int startcolumn)
		{
			ReInit(dstream, startline, startcolumn, 4096);
		}

		public virtual string GetImage()
		{
			if (bufpos >= tokenBegin)
			{
				return new string(buffer, tokenBegin, bufpos - tokenBegin + 1);
			}
			else
			{
				return new string(buffer, tokenBegin, bufsize - tokenBegin) + new string(buffer, 
					0, bufpos + 1);
			}
		}

		public virtual char[] GetSuffix(int len)
		{
			char[] ret = new char[len];
			if ((bufpos + 1) >= len)
			{
				System.Array.Copy(buffer, bufpos - len + 1, ret, 0, len);
			}
			else
			{
				System.Array.Copy(buffer, bufsize - (len - bufpos - 1), ret, 0, len - bufpos - 1);
				System.Array.Copy(buffer, 0, ret, len - bufpos - 1, bufpos + 1);
			}
			return ret;
		}

		public virtual void Done()
		{
			buffer = null;
			bufline = null;
			bufcolumn = null;
		}

		/// <summary>Method to adjust line and column numbers for the start of a token.</summary>
		public virtual void AdjustBeginLineColumn(int newLine, int newCol)
		{
			int start = tokenBegin;
			int len;
			if (bufpos >= tokenBegin)
			{
				len = bufpos - tokenBegin + inBuf + 1;
			}
			else
			{
				len = bufsize - tokenBegin + bufpos + 1 + inBuf;
			}
			int i = 0;
			int j = 0;
			int k = 0;
			int nextColDiff = 0;
			int columnDiff = 0;
			while (i < len && bufline[j = start % bufsize] == bufline[k = ++start % bufsize])
			{
				bufline[j] = newLine;
				nextColDiff = columnDiff + bufcolumn[k] - bufcolumn[j];
				bufcolumn[j] = newCol + columnDiff;
				columnDiff = nextColDiff;
				i++;
			}
			if (i < len)
			{
				bufline[j] = newLine++;
				bufcolumn[j] = newCol + columnDiff;
				while (i++ < len)
				{
					if (bufline[j = start % bufsize] != bufline[++start % bufsize])
					{
						bufline[j] = newLine++;
					}
					else
					{
						bufline[j] = newLine;
					}
				}
			}
			line = bufline[j];
			column = bufcolumn[j];
		}
	}
}
